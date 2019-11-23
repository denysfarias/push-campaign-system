using System.Collections.Generic;

namespace WebApi.PushCampaignService.Domain.DataStore
{
    public interface IDataStore<TData> where TData : class
    {
        IEnumerable<TData> FindAll();

        void Load(IEnumerable<TData> data);

        void Reset();
    }
}
