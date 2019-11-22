
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Services.Implementation
{
    public class MockCampaignRepository : ICampaignRepository
    {
        private readonly List<Campaign> _campaigns;

        public MockCampaignRepository()
        {
            _campaigns = new List<Campaign>();
        }

        public Campaign Create(Campaign campaign)
        {
            if (campaign == null)
                throw new ArgumentNullException();

            var keyExists = _campaigns.Any(c => c.Id == campaign.Id);
            if (keyExists)
                throw new ArgumentException("Key already exists!");

            if (campaign.Id <= 0)
            {
                int nextId;

                do
                {
                    nextId = new Random().Next();
                } while (nextId == 0 || _campaigns.Any(c => c.Id == nextId));

                campaign.Id = nextId;
            }

            _campaigns.Add(campaign);

            return campaign;
        }

        public IEnumerable<Campaign> FindAll()
        {
            return _campaigns.AsReadOnly();
        }

        public Campaign FindById(int id)
        {
            return _campaigns.FirstOrDefault(campaign => campaign.Id == id);
        }
    }
}
