using Domain.Caching;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider;
using Domain.PushNotificationProvider.Models;
using Domain.Services;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace PushCampaignWorker
{
    public class CampaignPusher : ICampaignPusher
    {
        private readonly ISetCache _setCache;
        private readonly IPushNotificationProviderFactory _pushNotificationProviderFactory;

        public CampaignPusher(ISetCache setCache, IPushNotificationProviderFactory pushNotificationProviderFactory)
        {
            _setCache = setCache;
            _pushNotificationProviderFactory = pushNotificationProviderFactory;
        }

        public async Task<CommandNotification> PushCampaign(Visit visit)
        {
            var readCacheResult = await _setCache.GetAll(visit.PlaceId.ToString());
            if (readCacheResult.IsInvalid)
                return new CommandNotification(readCacheResult);

            var pushCampaigns = readCacheResult.Object
                .Select(raw => JsonConvert.DeserializeObject<PushCampaign>(raw));

            IPushNotificationProvider provider;
            PushNotificationPayload payload;
            foreach (var pushCampaign in pushCampaigns)
            {
                provider = _pushNotificationProviderFactory.Create(pushCampaign.Provider);
                
                payload = new PushNotificationPayload()
                {
                    DeviceId = visit.DeviceId,
                    Message = pushCampaign.Message,
                    VisitId = visit.Id
                };

                provider.PushNotification(payload);
            }

            return new CommandNotification();
        }
    }
}
