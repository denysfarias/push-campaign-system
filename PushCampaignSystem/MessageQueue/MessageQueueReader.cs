using Domain.MessageQueue;
using Domain.Notifications.DataTransferObjects;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace MessageQueue
{
    public class MessageQueueReader<T> : IMessageQueueReader<T> where T : class
    {
        private const string ConsumerAlreadyExistsException = "Error when getting message from queue: there is a consumer reading the queue already.";

        private const bool MultipleAck = false;
        private const bool MultipleNack = false;
        private const int PrefetchSize = 0;
        private const int PrefetchCount = 1;
        private const bool GlobalQosConfiguration = false;

        public readonly QueueConfiguration Configuration;
        public readonly Func<T, string> MessageIdSelector;
        private IConnection Connection { get; set; }
        private IModel Channel { get; set; }
        private EventingBasicConsumer Consumer { get; set; }

        bool disposed = false;

        public MessageQueueReader(QueueConfiguration configuration, Func<T, string> messageIdSelector)
        {
            Configuration = configuration;
            MessageIdSelector = messageIdSelector;
        }

        public CommandNotification StartReading(ConsumeQueueHandler<T> consumeQueueHandler)
        {
            if (Consumer != null)
            {
                return new CommandNotification(property: null, message: ConsumerAlreadyExistsException, level: Domain.Notifications.Level.Error);
            }

            var commandResult = OpenConnection();
            if (commandResult.IsInvalid)
            {
                return commandResult;
            }

            try
            {
                var consumer = new EventingBasicConsumer(Channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = System.Text.Encoding.UTF8.GetString(body);
                    var data = JsonConvert.DeserializeObject<T>(message);

                    void ackHandler() => Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: MultipleAck);
                    void nackDelegate(bool requeueMessage) => Channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: MultipleNack, requeue: requeueMessage);

                    consumeQueueHandler(data, ackHandler, nackDelegate);
                };

                Channel.BasicConsume(queue: Configuration.Queue,
                                     autoAck: false,
                                     consumer: consumer);

                return new CommandNotification();
            }
            catch (Exception ex)
            {
                string erro = $"[READ ERROR] HostName: {Configuration.Hostname}, UserName: {Configuration.Username}"
                    + $", Password: {Configuration.Password}, VirtualHost: {Configuration.VirtualHost}";

                if (String.IsNullOrEmpty(ex.Message))
                    erro += $", Message: {ex.Message}";
                else if (ex.InnerException != null)
                    erro += $", Exception: {ex.InnerException.ToString()}";
                else if (ex.StackTrace != null)
                    erro += $", Trace: {ex.StackTrace.ToString()}";

                return new CommandNotification(property: null, message: erro, level: Domain.Notifications.Level.Error);
            }
        }

        private CommandNotification OpenConnection()
        {
            if (Channel != null)
                return new CommandNotification();

            try
            {
                var factory = new ConnectionFactory() 
                { 
                    HostName = Configuration.Hostname, 
                    UserName = Configuration.Username, 
                    Password = Configuration.Password, 
                    VirtualHost = Configuration.VirtualHost 
                };

                Connection = factory.CreateConnection();
                Channel = Connection.CreateModel();

                Channel.QueueDeclare(queue: Configuration.Queue,
                    durable: Configuration.DurableQueue,
                    exclusive: Configuration.ExclusiveQueue,
                    autoDelete: Configuration.AutoDeleteQueue,
                    arguments: null);

                Channel.BasicQos(prefetchSize: PrefetchSize, prefetchCount: PrefetchCount, global: GlobalQosConfiguration);

                return new CommandNotification();
            }
            catch (Exception ex)
            {
                string erro = $"[OPEN QUEUE ERROR] HostName: {Configuration.Hostname}, UserName: {Configuration.Username}"
                    + $", Password: {Configuration.Password}, VirtualHost: {Configuration.VirtualHost}";

                if (String.IsNullOrEmpty(ex.Message))
                    erro += $", Message: {ex.Message}";
                else if (ex.InnerException != null)
                    erro += $", Exception: {ex.InnerException.ToString()}";
                else if (ex.StackTrace != null)
                    erro += $", Trace: {ex.StackTrace.ToString()}";

                return new CommandNotification(property: null, message: erro, level: Domain.Notifications.Level.Error);
            }
        }

        public void FinishReading()
        {
            Dispose();
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                Consumer = null;

                Channel.Dispose();
                Channel = null;

                Connection.Dispose();
                Connection = null;
            }

            // Free any unmanaged objects here.
            
            disposed = true;
        }
    }
}
