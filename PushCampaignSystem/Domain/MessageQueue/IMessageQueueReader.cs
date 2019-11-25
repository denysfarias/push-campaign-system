using Domain.Notifications.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace Domain.MessageQueue
{
    public delegate void AckHandler();
    public delegate void NackHandler(bool requeue);
    public delegate Task ConsumeQueueHandler<T>(T data, AckHandler ackHandler, NackHandler nackHandler);

    public interface IMessageQueueReader<T> : IDisposable where T : class
    {
        Task<CommandNotification> StartReadingAsync(ConsumeQueueHandler<T> consumeQueueHandler);

        void FinishReading();
    }
}
