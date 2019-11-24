using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Visit
    {
        [Range(1, int.MaxValue)]
        public int id { get; set; }

        [Required]
        public string device_id { get; set; }

        [Range(1, int.MaxValue)]
        public int place_id { get; set; }
    }
}
