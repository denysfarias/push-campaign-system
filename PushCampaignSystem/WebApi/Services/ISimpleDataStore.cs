using System.Collections.Generic;

namespace WebApi.Services
{
    public interface ISimpleDataStore<TData> where TData : class
    {
        IEnumerable<TData> FindAll();

        void Load(IEnumerable<TData> data);

        void Reset();
    }
}
