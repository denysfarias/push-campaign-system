using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Place
    {
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int PlaceId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
