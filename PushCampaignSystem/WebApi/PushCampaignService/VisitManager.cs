using System.Collections.Generic;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;

namespace WebApi.PushCampaignService
{
    public class VisitManager : IVisitManager
    {
        private readonly ISimpleDataStore<Visit> _visitStore;

        private readonly ISimpleDataStore<Campaign> _campaignStore;

        public VisitManager(ISimpleDataStore<Visit> visitStore, ISimpleDataStore<Campaign> campaignStore)
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
