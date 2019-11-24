using System.Collections.Generic;

namespace Domain.MessageQueue
{
    public interface IMessageQueueWriter<T> where T : class
    {
        void Post(T content);
        void Post(IList<T> content);
    }
}
