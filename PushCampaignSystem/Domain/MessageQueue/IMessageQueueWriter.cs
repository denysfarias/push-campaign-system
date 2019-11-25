using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;

namespace Domain.MessageQueue
{
    public interface IMessageQueueWriter<T> where T : class
    {
        CommandNotification Post(T content);
        CommandNotification Post(IList<T> content);
    }
}
