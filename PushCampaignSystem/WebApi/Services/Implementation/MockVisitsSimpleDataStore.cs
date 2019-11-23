using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services.Implementation
{
    public class MockVisitsSimpleDataStore : ISimpleDataStore<Visit>
    {
        private readonly List<Visit> _visits;

        public MockVisitsSimpleDataStore()
        {
            _visits = new List<Visit>();
        }

        public MockVisitsSimpleDataStore(IEnumerable<Visit> visits)
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
