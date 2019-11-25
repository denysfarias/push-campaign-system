using Domain.Notifications;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using Entities = Domain.DataStore.Entities;

namespace WebApi.Models
{
    public class Campaign
    {
        //[Range(minimum: 1, maximum: int.MaxValue)]
        public int id { get; set; }

        //[Required]
        public string provider { get; set; }

        //[Required]
        public string push_message { get; set; }

        public IEnumerable<Place> targeting { get; set; }

        public CommandNotification Validate()
        {
            var notifications = new List<Notification>();

            if (id <= 0)
                notifications.Add(new Notification(property: nameof(id), message: "Invalid id"));

            if (string.IsNullOrWhiteSpace(provider))
                notifications.Add(new Notification(property: nameof(provider), message: "Invalid provider"));

            if (string.IsNullOrWhiteSpace(push_message))
                notifications.Add(new Notification(property: nameof(provider), message: "Invalid push_message"));

            var targetValidations = targeting.Select(target => target.Validate()).ToArray();

            var result = new CommandNotification(notifications);
            result.AddNotifications(targetValidations);
            return result;
        }
    }

    public static class CampaignMapper
    {
        public static Campaign ToModel(Entities.Campaign entity)
        {
            return new Campaign()
            {
                id = entity.Id,
                provider = entity.Provider,
                push_message = entity.PushMessage,
                targeting = entity.Targeting.Select(target => PlaceMapper.ToModel(target)).ToList()
            };
        }

        public static Domain.DataStore.Entities.Campaign ToEntity(Campaign model)
        {
            return new Domain.DataStore.Entities.Campaign()
            {
                Id = model.id,
                Provider = model.provider,
                PushMessage = model.push_message,
                Targeting = model.targeting.Select(target => PlaceMapper.ToEntity(target)).ToList()
            };
        }
    }
}
