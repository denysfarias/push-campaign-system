using System.Collections.Generic;

namespace WebApi.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        public string Provider { get; set; }

        public string PushMessage { get; set; }

        public IEnumerable<Place> Targeting { get; set; }
    }
}
