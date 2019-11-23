using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.PushCampaignService.DataStore;
using Xunit;

namespace Tests.WebApi
{
    public class CampaignsControllerTests
    {
        private CampaignsController SetupController()
        {
            var samples = CampaignSamples.Get();
            var dataStore = new MockCampaignSimpleDataStore(samples);
            return new CampaignsController(logger: null, campaignDataStore: dataStore);
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
                PushMessage = "Teste j� essa aventura!",
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
