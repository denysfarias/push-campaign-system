using System.ComponentModel.DataAnnotations;
using Entities = Domain.DataStore.Entities;

namespace WebApi.Models
{
    public class Place
    {
        [Range(minimum: 1, maximum: int.MaxValue)]
        public int place_id { get; set; }

        [Required]
        public string name { get; set; }
    }

    public static class PlaceMapper
    {
        public static Place ToModel(Entities.Place entity)
        {
            return new Place()
            {
                place_id = entity.PlaceId,
                name = entity.Name
            };
        }

        public static Entities.Place ToEntity(Place model)
        {
            return new Entities.Place()
            {
                PlaceId = model.place_id,
                Name = model.name
            };
        }
    }
}
