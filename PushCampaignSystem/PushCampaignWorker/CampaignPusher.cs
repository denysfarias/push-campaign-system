using Domain.Caching;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider;
using Domain.PushNotificationProvider.Models;
using Domain.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public async Task<CommandNotification> PushCampaignAsync(Visit visit)
        {
            var readCacheResult = await _setCache.GetAllAsync(visit.PlaceId.ToString());
            if (readCacheResult.IsInvalid)
                return new CommandNotification(readCacheResult);

            var pushCampaigns = readCacheResult.Object
                .Select(raw => JsonConvert.DeserializeObject<PushCampaign>(raw));

            if (!pushCampaigns.Any())
            {
                pushCampaigns = new List<PushCampaign>()
                {
                    new PushCampaign()
                    {
                        Message = string.Empty,
                        Provider = string.Empty
                    }
                };
            }

            ObjectWithNotification<IPushNotificationProvider> providerResult;
            IPushNotificationProvider provider;
            PushNotificationPayload payload;
            CommandNotification pushNotification;
            foreach (var pushCampaign in pushCampaigns)
            {
                providerResult = _pushNotificationProviderFactory.Create(pushCampaign.Provider);

                if (providerResult.IsInvalid)
                    return new CommandNotification(providerResult);

                provider = providerResult.Object;

                payload = new PushNotificationPayload()
                {
                    DeviceId = visit.DeviceId,
                    Message = pushCampaign.Message,
                    VisitId = visit.Id
                };

                pushNotification = provider.PushNotification(payload);
                if (pushNotification.IsInvalid)
                {
                    return new CommandNotification(pushNotification);
                }
            }

            return new CommandNotification();
        }
    }
}
