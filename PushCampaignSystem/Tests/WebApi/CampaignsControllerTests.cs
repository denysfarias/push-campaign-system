using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Services;
using WebApi.Services.Implementation;
using Xunit;

namespace Tests
{
    public class CampaignsControllerTests
    {
        private IList<Campaign> _samples;
        private ICampaignRepository _repository;

        private void Setup()
        {
            _repository = new MockCampaignRepository();
            _repository.Create(new Campaign()
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
            });
            _repository.Create(new Campaign()
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
            });
        }

        [Fact]
        public void GetAll_ReturnsCampaigns()
        {
            Setup();

            var controller = new CampaignsController(null, _repository);
            var response = controller.GetAll();

            var responseValue = Assert.IsAssignableFrom<IEnumerable<Campaign>>(response.Value);
            Assert.NotNull(responseValue);
        }

        [Fact]
        public void GetById_ReturnsCampaignById()
        {
            Setup();
            var id = 1;

            var controller = new CampaignsController(null, _repository);
            var response = controller.GetById(id);

            var responseValue = Assert.IsAssignableFrom<Campaign>(response.Value);
            Assert.NotNull(responseValue);
            Assert.Equal(id, responseValue.Id);
        }

        [Fact]
        public void GetById_ReturnsNotFoundResult()
        {
            Setup();
            var id = int.MaxValue;

            var controller = new CampaignsController(null, _repository);
            var response = controller.GetById(id);

            Assert.IsAssignableFrom<NotFoundResult>(response.Result);
        }
    }
}
