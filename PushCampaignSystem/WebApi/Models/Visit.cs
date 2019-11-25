using Domain.Notifications;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using Entities = Domain.DataStore.Entities;

namespace WebApi.Models
{
    public class Visit
    {
        //[Range(1, int.MaxValue)]
        public int id { get; set; }

        //[Required]
        public string device_id { get; set; }

        //[Range(1, int.MaxValue)]
        public int place_id { get; set; }

        public CommandNotification Validate()
        {
            var notifications = new List<Notification>();

            if (id <= 0)
                notifications.Add(new Notification(property: nameof(id), message: "Invalid id"));

            if (string.IsNullOrWhiteSpace(device_id))
                notifications.Add(new Notification(property: nameof(device_id), message: "Invalid device_id"));

            if (place_id <= 0)
                notifications.Add(new Notification(property: nameof(place_id), message: "Invalid place_id"));

            return new CommandNotification(notifications);
        }
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
