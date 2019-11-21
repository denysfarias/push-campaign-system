using System.Collections.Generic;

namespace WebApi.Models
{
    public class Campaign
    {
        public long Id { get; set; }

        public string Provider { get; set; }

        public IEnumerable<Campaign> Targeting { get; set; }
    }
}
