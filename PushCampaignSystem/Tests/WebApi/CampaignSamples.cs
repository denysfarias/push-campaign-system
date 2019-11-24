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
                    id = 1,
                    provider = "localytics",
                    push_message = "70% OFF! Order our best pizza: In Loco Pizza!",
                    targeting = new List<Place>()
                    {
                        new Place()
                        {
                            place_id = 79,
                            name = "In Loco Pizzas – Recife Antigo"
                        },
                        new Place()
                        {
                            place_id = 22,
                            name = "In Loco Pizzas – Casa Forte"
                        }
                    }
                },
                new Campaign()
                {
                    id = 2,
                    provider = "mixpanel",
                    push_message = "Peça o drink do dia! Se beber, não dirija. :)",
                    targeting = new List<Place>()
                    {
                        new Place()
                        {
                            place_id = 33,
                            name = "Bar da esquina"
                        },
                        new Place()
                        {
                            place_id = 90,
                            name = "In Loco Drinks Bar"
                        },
                        new Place()
                        {
                            place_id = 7624,
                            name = "In Loco Restaurante"
                        }
                    }
                }
            };
    }
}
