using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider;
using Domain.PushNotificationProvider.Models;
using Domain.Services;
using System.Collections.Generic;

namespace WebApi.PushCampaignService
{
    public class VisitManager : IVisitManager
    {
        private readonly IDataStore<Visit> _visitStore;

        private readonly ICampaignSearch _campaignSearch;

        private readonly IPushNotificationProviderFactory _pushNotificationProviderFactory;

        public VisitManager(IDataStore<Visit> visitStore, ICampaignSearch campaignSearch, IPushNotificationProviderFactory pushNotificationProviderFactory)
        {
            _visitStore = visitStore;
            _campaignSearch = campaignSearch;
            _pushNotificationProviderFactory = pushNotificationProviderFactory;
        }

        public ObjectWithNotification<IEnumerable<Visit>> GetAll()
        {
            return _visitStore.FindAll();
        }

        public CommandNotification Load(IEnumerable<Visit> visits)
        {
            PushNotifications(visits);
            return _visitStore.Load(visits);
        }

        private CommandNotification PushNotifications(IEnumerable<Visit> visits)
        {
            foreach (var visit in visits)
            {
                var pushCampaignListResult = _campaignSearch.FindMessagesForPlace(visit.PlaceId);

                if (pushCampaignListResult.IsInvalid)
                    return new CommandNotification(pushCampaignListResult);

                foreach (var pushCampaign in pushCampaignListResult.Object)
                {
                    var providerResult = _pushNotificationProviderFactory.Create(pushCampaign.Provider);

                    if (providerResult.IsInvalid)
                        return new CommandNotification(providerResult);

                    var provider = providerResult.Object;

                    var payload = new PushNotificationPayload() { DeviceId = visit.DeviceId, Message = pushCampaign.Message, VisitId = visit.Id };

                    provider.PushNotification(payload);
                }
            }

            return new CommandNotification();
        }
    }
}
