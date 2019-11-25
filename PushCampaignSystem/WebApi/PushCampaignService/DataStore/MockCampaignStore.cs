using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.PushCampaignService.DataStore
{
    public class MockCampaignStore : IDataStore<Campaign>
    {
        private readonly List<Campaign> _campaigns;

        public MockCampaignStore()
        {
            _campaigns = new List<Campaign>();
        }

        public MockCampaignStore(IEnumerable<Campaign> campaigns)
        {
            _campaigns = campaigns.ToList();
        }

        public CommandNotification Load(IEnumerable<Campaign> data)
        {
            var hasDuplicate = _campaigns.Any(stored => data.Any(toStore => stored.Id == toStore.Id));

            if (hasDuplicate)
                return new CommandNotification(property: string.Empty, message: "Remove duplicate campaign to proceed.", level: Domain.Notifications.Level.Error);

            _campaigns.AddRange(data);
            return new CommandNotification();
        }

        public ObjectWithNotification<IEnumerable<Campaign>> FindAll()
        {
            var result = _campaigns.AsReadOnly();
            return new ObjectWithNotification<IEnumerable<Campaign>>(result);
        }

        public CommandNotification Reset()
        {
            _campaigns.Clear();
            return new CommandNotification();
        }
    }
}
