
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services.Implementation
{
    public class MockCampaignSimpleDataStore : ICampaignSimpleDataStore
    {
        private readonly List<Campaign> _campaigns;

        public MockCampaignSimpleDataStore()
        {
            _campaigns = new List<Campaign>();
        }

        public MockCampaignSimpleDataStore(IEnumerable<Campaign> campaigns)
        {
            _campaigns = campaigns.ToList();
        }

        public void Load(IEnumerable<Campaign> campaigns)
        {
            if (campaigns == null)
                throw new ArgumentNullException();

            //TODO: validate input

            _campaigns.AddRange(campaigns);
        }

        public IEnumerable<Campaign> FindAll()
        {
            return _campaigns.AsReadOnly();
        }

        public void Reset()
        {
            _campaigns.Clear();
        }
    }
}
