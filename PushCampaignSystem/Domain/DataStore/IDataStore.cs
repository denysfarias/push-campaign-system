using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;

namespace Domain.DataStore
{
    public interface IDataStore<TData> where TData : class
    {
        ObjectWithNotification<IEnumerable<TData>> FindAll();

        CommandNotification Load(IEnumerable<TData> data);

        CommandNotification Reset();
    }
}
