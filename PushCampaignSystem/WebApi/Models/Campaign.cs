using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Entities = Domain.DataStore.Entities;

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
