using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
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

        public ObjectWithNotification<IEnumerable<PushCampaign>> FindMessagesForPlace(int placeId)
        {
            var retrieved = _campaignStore.FindAll();

            if (retrieved.IsInvalid)
                return new ObjectWithNotification<IEnumerable<PushCampaign>>(null, retrieved);

            var mapped = retrieved.Object
                .Where(campaign => campaign.Targeting.Any(target => target.PlaceId == placeId))
                .Select(campaign => new PushCampaign() { Message = campaign.PushMessage, Provider = campaign.Provider })
                .ToList();

            return new ObjectWithNotification<IEnumerable<PushCampaign>>(mapped);
        }
    }
}
