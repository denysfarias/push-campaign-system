using System.Collections.Generic;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;
using WebApi.PushCampaignService.Domain.DataStore;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;

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

        public IEnumerable<Visit> GetAll()
        {
            return _visitStore.FindAll();
        }

        public void Load(IEnumerable<Visit> visits)
        {
            _visitStore.Load(visits);

            PushNotifications(visits);
        }

        private void PushNotifications(IEnumerable<Visit> visits)
        {
            foreach (var visit in visits)
            {
                var pushCampaignList = _campaignSearch.FindMessagesForPlace(visit.place_id);

                foreach (var pushCampaign in pushCampaignList)
                {
                    var provider = _pushNotificationProviderFactory.Create(pushCampaign.Provider);

                    var payload = new PushNotificationPayload() { DeviceId = visit.device_id, Message = pushCampaign.Message, VisitId = visit.id };

                    provider.PushNotification(payload);
                }
            }
        }
    }
}
