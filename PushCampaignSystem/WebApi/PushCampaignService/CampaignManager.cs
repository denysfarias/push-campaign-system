using System.Collections.Generic;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;
using WebApi.PushCampaignService.Domain.DataStore;

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
