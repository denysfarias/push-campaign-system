using System.Collections.Generic;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;

namespace WebApi.PushCampaignService
{
    public class CampaignManager : ICampaignManager
    {
        private readonly ISimpleDataStore<Campaign> _campaignStore;

        public CampaignManager(ISimpleDataStore<Campaign> campaignStore)
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
