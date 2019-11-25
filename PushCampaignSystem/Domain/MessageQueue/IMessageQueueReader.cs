using Domain.Notifications.DataTransferObjects;
using System;

namespace Domain.MessageQueue
{
    public delegate void AckHandler();
    public delegate void NackHandler(bool requeue);
    public delegate void ConsumeQueueHandler<T>(T data, AckHandler ackHandler, NackHandler nackHandler);

    public interface IMessageQueueReader<T> : IDisposable where T : class
    {
        CommandNotification StartReading(ConsumeQueueHandler<T> consumeQueueHandler);

        void FinishReading();
    }
}
