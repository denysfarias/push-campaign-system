using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.PushCampaignService.Domain.DataStore;

namespace WebApi.PushCampaignService.DataStore
{
    public class MockVisitStore : IDataStore<Visit>
    {
        private readonly List<Visit> _visits;

        public MockVisitStore()
        {
            _visits = new List<Visit>();
        }

        public MockVisitStore(IEnumerable<Visit> visits)
        {
            _visits = visits.ToList();
        }

        public void Load(IEnumerable<Visit> data)
        {
            //TODO: validate input

            _visits.AddRange(data);
        }

        public IEnumerable<Visit> FindAll()
        {
            return _visits.AsReadOnly();
        }

        public void Reset()
        {
            _visits.Clear();
        }
    }
}
