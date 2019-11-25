using Domain.MessageQueue;
using MessageQueue;
using System;

namespace WebApi.PushCampaignService
{
    public class MessageQueueWriterResolver
    {
        public IMessageQueueWriter<TPayload> Resolve<TPayload>(object configuration) where TPayload : class
        {
            Func<TPayload, string> geradorId = _ => Guid.NewGuid().ToString();
            return new MessageQueueWriter<TPayload>((QueueConfiguration)configuration, geradorId);
        }
    }
}
