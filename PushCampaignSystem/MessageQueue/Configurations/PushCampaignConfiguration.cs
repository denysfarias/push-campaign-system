namespace MessageQueue.Configurations
{
    public class PushCampaignConfiguration : QueueConfiguration
    {
        public PushCampaignConfiguration()
        {
            AutoDeleteQueue = false;
            DurableQueue = true;
            Exchange = string.Empty;
            ExclusiveQueue = false;
            PersistentChannel = true;
            Queue = "push-campaign-queue";
            RoutingKey = "push-campaign-queue";
            Password = GeneralData.PASSWORD;
            Username = GeneralData.USERNAME;
            Hostname = GeneralData.HOSTNAME;
            VirtualHost = GeneralData.VIRTUALHOST;
        }
    }
}
