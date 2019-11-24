using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Place
    {
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int place_id { get; set; }

        [Required]
        public string name { get; set; }
    }
}
