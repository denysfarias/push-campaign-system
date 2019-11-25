using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.MessageQueue;
using Domain.Notifications.DataTransferObjects;
using Domain.Services;
using System.Collections.Generic;

namespace WebApi.PushCampaignService
{
    public class DistributedVisitManager : IVisitManager
    {
        private readonly IDataStore<Visit> _visitStore;
        private readonly IMessageQueueWriter<Visit> _pushCampaignQueueWriter;

        public DistributedVisitManager(IDataStore<Visit> visitStore, 
            MessageQueueWriterResolver messageQueueWriterResolver)
        {
            _visitStore = visitStore;
            _pushCampaignQueueWriter = messageQueueWriterResolver.Resolve<Visit>(new MessageQueue.Configurations.PushCampaignConfiguration());
        }

        public ObjectWithNotification<IEnumerable<Visit>> GetAll()
        {
            return _visitStore.FindAll();
        }

        public CommandNotification Load(IEnumerable<Visit> visits)
        {
            var result = _visitStore.Load(visits);
            if (result.IsInvalid)
                return result;

            return _pushCampaignQueueWriter.Post(visits);
        }
    }
}
