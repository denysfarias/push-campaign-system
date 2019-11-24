using System.Collections.Generic;

namespace Domain.DataStore
{
    public interface IDataStore<TData> where TData : class
    {
        IEnumerable<TData> FindAll();

        void Load(IEnumerable<TData> data);

        void Reset();
    }
}
