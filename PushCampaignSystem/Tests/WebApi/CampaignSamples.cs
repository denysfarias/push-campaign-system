using System.Collections.Generic;
using WebApi.Models;

namespace Tests.WebApi
{
    public static class CampaignSamples
    {
        public static IEnumerable<Campaign> Get()
        {
            return Samples.AsReadOnly();
        }

        private static readonly List<Campaign> Samples = new List<Campaign>()
            {
                new Campaign()
                {
                    Id = 1,
                    Provider = "localytics",
                    PushMessage = "70% OFF! Order our best pizza: In Loco Pizza!",
                    Targeting = new List<Place>()
                    {
                        new Place()
                        {
                            PlaceId = 79,
                            Name = "In Loco Pizzas – Recife Antigo"
                        },
                        new Place()
                        {
                            PlaceId = 22,
                            Name = "In Loco Pizzas – Casa Forte"
                        }
                    }
                },
                new Campaign()
                {
                    Id = 2,
                    Provider = "mixpanel",
                    PushMessage = "Peça o drink do dia! Se beber, não dirija. :)",
                    Targeting = new List<Place>()
                    {
                        new Place()
                        {
                            PlaceId = 33,
                            Name = "Bar da esquina"
                        },
                        new Place()
                        {
                            PlaceId = 90,
                            Name = "In Loco Drinks Bar"
                        },
                        new Place()
                        {
                            PlaceId = 7624,
                            Name = "In Loco Restaurante"
                        }
                    }
                }
            };
    }
}
