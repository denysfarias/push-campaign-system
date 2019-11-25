namespace MessageQueue.Configurations
{
    public class IndexCampaignConfiguration : QueueConfiguration
    {
        public IndexCampaignConfiguration()
        {
            AutoDeleteQueue = false;
            DurableQueue = true;
            Exchange = string.Empty;
            ExclusiveQueue = false;
            PersistentChannel = true;
            Queue = "index-campaign-queue";
            RoutingKey = "index-campaign-queue";
            Password = GeneralData.PASSWORD;
            Username = GeneralData.USERNAME;
            Hostname = GeneralData.HOSTNAME;
            VirtualHost = GeneralData.VIRTUALHOST;
        }
    }
}
