namespace MessageQueue
{
    public class QueueConfiguration
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Hostname { get; set; }

        public string VirtualHost { get; set; }

        public string Queue { get; set; }

        public string RoutingKey { get; set; }

        public bool DurableQueue { get; set; }

        public bool ExclusiveQueue { get; set; }

        public bool AutoDeleteQueue { get; set; }

        public bool PersistentChannel { get; set; }

        public string Exchange { get; set; }
    }
}
