using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
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

        public ObjectWithNotification<IEnumerable<Campaign>> GetAll()
        {
            return _campaignStore.FindAll();
        }

        public CommandNotification Load(IEnumerable<Campaign> campaigns)
        {
            return _campaignStore.Load(campaigns);
        }
    }
}
