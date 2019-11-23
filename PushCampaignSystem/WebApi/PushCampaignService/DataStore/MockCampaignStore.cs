using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.PushCampaignService.Domain.DataStore;

namespace WebApi.PushCampaignService.DataStore
{
    public class MockCampaignStore : IDataStore<Campaign>
    {
        private readonly List<Campaign> _campaigns;

        public MockCampaignStore()
        {
            _campaigns = new List<Campaign>();
        }

        public MockCampaignStore(IEnumerable<Campaign> campaigns)
        {
            _campaigns = campaigns.ToList();
        }

        public void Load(IEnumerable<Campaign> data)
        {
            //TODO: validate input

            _campaigns.AddRange(data);
        }

        public IEnumerable<Campaign> FindAll()
        {
            return _campaigns.AsReadOnly();
        }

        public void Reset()
        {
            _campaigns.Clear();
        }
    }
}
