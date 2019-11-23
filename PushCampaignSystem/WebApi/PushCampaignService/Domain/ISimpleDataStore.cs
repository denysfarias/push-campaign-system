using System.Collections.Generic;

namespace WebApi.PushCampaignService.Domain
{
    public interface ISimpleDataStore<TData> where TData : class
    {
        IEnumerable<TData> FindAll();

        void Load(IEnumerable<TData> data);

        void Reset();
    }
}
