using System.Collections.Generic;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;
using WebApi.PushCampaignService.Domain.DataStore;

namespace WebApi.PushCampaignService
{
    public class VisitManager : IVisitManager
    {
        private readonly IDataStore<Visit> _visitStore;

        private readonly IDataStore<Campaign> _campaignStore;

        public VisitManager(IDataStore<Visit> visitStore, IDataStore<Campaign> campaignStore)
        {
            _visitStore = visitStore;
            _campaignStore = campaignStore;
        }

        public IEnumerable<Visit> GetAll()
        {
            return _visitStore.FindAll();
        }

        public void Load(IEnumerable<Visit> visits)
        {
            _visitStore.Load(visits);
        }
    }
}
