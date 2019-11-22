using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Place
    {
        public int PlaceId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
