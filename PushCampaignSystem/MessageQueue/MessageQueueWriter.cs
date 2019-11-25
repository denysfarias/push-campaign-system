using Domain.MessageQueue;
using Domain.Notifications.DataTransferObjects;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace MessageQueue
{
    public class MessageQueueWriter<T> : IMessageQueueWriter<T> where T : class
    {
        public readonly QueueConfiguration Configuration;

        public readonly Func<T, string> MessageIdSelector;

        public MessageQueueWriter(QueueConfiguration configuration, Func<T, string> messageIdSelector)
        {
            Configuration = configuration;
            MessageIdSelector = messageIdSelector;
        }

        public CommandNotification Post(T content)
        {
            return PostCommon(channel =>
            {
                PostContent(channel, content);
            });
        }

        public CommandNotification Post(IEnumerable<T> contentList)
        {
            return PostCommon(channel =>
            {
                foreach (var content in contentList)
                {
                    PostContent(channel, content);
                }
            });
        }

        private CommandNotification PostCommon(Action<IModel> postContentAction)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration.Hostname,
                    UserName = Configuration.Username,
                    Password = Configuration.Password,
                    VirtualHost = Configuration.VirtualHost
                };

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare
                            (
                            queue: Configuration.Queue,
                            durable: Configuration.DurableQueue,
                            exclusive: Configuration.ExclusiveQueue,
                            autoDelete: Configuration.AutoDeleteQueue,
                            arguments: null
                            );

                        postContentAction(channel);

                        return new CommandNotification();
                    }
                }
            }
            catch (Exception ex)
            {
                string erro = $"[POST ERROR] HostName: {Configuration.Hostname}, UserName: {Configuration.Username}"
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

        private void PostContent(IModel channel, T content)
        {
            var json = JsonConvert.SerializeObject(content);
            var message = System.Text.Encoding.UTF8.GetBytes(json);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = Configuration.PersistentChannel;
            properties.MessageId = MessageIdSelector(content);

            channel.BasicPublish(exchange: Configuration.Exchange, routingKey: Configuration.RoutingKey, basicProperties: properties, body: message);
        }
    }
}
