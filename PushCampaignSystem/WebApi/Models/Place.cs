using Domain.Notifications;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using Entities = Domain.DataStore.Entities;

namespace WebApi.Models
{
    public class Place
    {
        //[Range(minimum: 1, maximum: int.MaxValue)]
        public int place_id { get; set; }

        //[Required]
        public string name { get; set; }

        public CommandNotification Validate()
        {
            var notifications = new List<Notification>();

            if (place_id <= 0)
                notifications.Add(new Notification(property: nameof(place_id), message: "Invalid place_id"));

            if (string.IsNullOrWhiteSpace(name))
                notifications.Add(new Notification(property: nameof(name), message: "Invalid name"));

            return new CommandNotification(notifications);
        }
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
