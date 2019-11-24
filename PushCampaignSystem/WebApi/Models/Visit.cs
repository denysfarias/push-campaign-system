using System.ComponentModel.DataAnnotations;
using Entities = Domain.DataStore.Entities;

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

    public static class VisitMapper
    {
        public static Visit ToModel(Entities.Visit entity)
        {
            return new Visit()
            {
                id = entity.Id,
                device_id = entity.DeviceId,
                place_id = entity.PlaceId
            };
        }

        public static Entities.Visit ToEntity(Visit model)
        {
            return new Entities.Visit()
            {
                Id = model.id,
                DeviceId = model.device_id,
                PlaceId = model.place_id
            };
        }
    }
}
