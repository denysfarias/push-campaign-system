using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.MessageQueue;
using Domain.PushNotificationProvider;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IEnumerable<Visit> GetAll()
        {
            return _visitStore.FindAll();
        }

        public void Load(IEnumerable<Visit> visits)
        {
            var result = _pushCampaignQueueWriter.Post(visits);

            if (result.IsInvalid)
                throw new Exception();

            _visitStore.Load(visits);
        }
    }
}
