using Domain.Caching;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace IndexCampaignWorker
{
    public class CampaignIndexer : ICampaignIndexer
    {
        private readonly ISetCache _setCache;

        public CampaignIndexer(ISetCache setCache)
        {
            this._setCache = setCache;
        }

        public async Task<CommandNotification> IndexCampaignAsync(Campaign campaign)
        {
            foreach (var target in campaign.Targeting)
            {
                var placeId = target.PlaceId;
                var pushCampaign = new PushCampaign()
                {
                    Message = campaign.PushMessage,
                    Provider = campaign.Provider
                };

                var payload = JsonConvert.SerializeObject(pushCampaign);

                var addResult = await _setCache.AddOrAppendAsync(placeId.ToString(), payload);

                if (addResult.IsInvalid)
                    return addResult;

                //var getResult = await _setCache.GetAll(placeId.ToString()); // Testing           
            }

            return new CommandNotification();
        }
    }
}
