using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.PushNotificationProvider.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.PushCampaignService.DataStore
{
    public class SimpleCampaignSearch : ICampaignSearch
    {
        private readonly IDataStore<Campaign> _campaignStore;

        public SimpleCampaignSearch(IDataStore<Campaign> campaignStore)
        {
            _campaignStore = campaignStore;
        }

        public IEnumerable<PushCampaign> FindMessagesForPlace(int placeId)
        {
            return _campaignStore.FindAll()
                .Where(campaign => campaign.Targeting.Any(target => target.PlaceId == placeId))
                .Select(campaign => new PushCampaign() { Message = campaign.PushMessage, Provider = campaign.Provider })
                .ToList();
        }
    }
}
