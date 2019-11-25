using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.MessageQueue;
using Domain.Notifications.DataTransferObjects;
using Domain.Services;
using System.Collections.Generic;

namespace WebApi.PushCampaignService
{
    public class DistributedCampaignManager : ICampaignManager
    {
        private readonly IDataStore<Campaign> _campaignStore;
        private readonly IMessageQueueWriter<Campaign> _indexCampaignQueueWriter;

        public DistributedCampaignManager(IDataStore<Campaign> campaignStore, 
            MessageQueueWriterResolver messageQueueWriterResolver)
        {
            _campaignStore = campaignStore;
            _indexCampaignQueueWriter = messageQueueWriterResolver.Resolve<Campaign>(new MessageQueue.Configurations.IndexCampaignConfiguration());
        }

        public ObjectWithNotification<IEnumerable<Campaign>> GetAll()
        {
            return _campaignStore.FindAll();
        }

        public CommandNotification Load(IEnumerable<Campaign> campaigns)
        {
            var indexingResult = _indexCampaignQueueWriter.Post(campaigns);
            if (indexingResult.IsInvalid)
                return indexingResult;

            return _campaignStore.Load(campaigns);
        }
    }
}
