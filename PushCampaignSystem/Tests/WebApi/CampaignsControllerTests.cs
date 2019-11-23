using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services.Implementation;
using Xunit;

namespace Tests
{
    public class CampaignsControllerTests
    {
        private readonly IEnumerable<Campaign> Sample;

        public CampaignsControllerTests()
        {
            Sample = new List<Campaign>()
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

        private CampaignsController SetupController()
        {
            var repository = new MockCampaignSimpleDataStore(Sample);
            return new CampaignsController(logger: null, campaignRepository: repository);
        }

        [Fact]
        public void GetAll_ReturnsCampaigns()
        {
            var controller = SetupController();

            var response = controller.GetAll();

            var responseValue = Assert.IsAssignableFrom<IEnumerable<Campaign>>(response.Value);
            Assert.NotNull(responseValue);
        }

        [Fact]
        public void Load_ReturnsOK()
        {
            var controller = SetupController();
            var campaign = new Campaign()
            {
                Id = 3,
                Provider = "Test-Provider-4x4",
                PushMessage = "Teste já essa aventura!",
                Targeting = new List<Place>()
                {
                    new Place()
                    {
                        PlaceId = 1,
                        Name = "Clube do Teste"
                    },
                    new Place()
                    {
                        PlaceId = 2,
                        Name = "Test Coffee"
                    }
                }
            };

            var response = controller.PostBatch(new List<Campaign>() { campaign });

            Assert.IsAssignableFrom<OkResult>(response);
        }

        [Fact]
        public void DeleteAll_ReturnsOK()
        {
            var controller = SetupController();

            var response = controller.DeleteAll();

            Assert.IsAssignableFrom<OkResult>(response);
        }
    }
}
