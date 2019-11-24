using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.Services;
using System.Collections.Generic;

namespace WebApi.PushCampaignService
{
    public class CampaignManager : ICampaignManager
    {
        private readonly IDataStore<Campaign> _campaignStore;

        public CampaignManager(IDataStore<Campaign> campaignStore)
        {
            _campaignStore = campaignStore;
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _campaignStore.FindAll();
        }

        public void Load(IEnumerable<Campaign> campaigns)
        {
            _campaignStore.Load(campaigns);
        }
    }
}
