using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Campaign
    {
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int id { get; set; }

        [Required]
        public string provider { get; set; }

        [Required]
        public string push_message { get; set; }

        public IEnumerable<Place> targeting { get; set; }
    }
}
