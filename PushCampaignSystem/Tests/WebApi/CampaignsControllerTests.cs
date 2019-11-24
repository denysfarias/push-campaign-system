using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.PushCampaignService;
using WebApi.PushCampaignService.DataStore;
using Xunit;

namespace Tests.WebApi
{
    public class CampaignsControllerTests
    {
        private CampaignsController SetupController()
        {
            var samples = CampaignSamples.Get();
            var dataStore = new MockCampaignStore(samples);
            var manager = new CampaignManager(dataStore);
            return new CampaignsController(manager, logger: null);
        }

        [Fact]
        public void GetAll_ReturnsCampaigns()
        {
            var controller = SetupController();

            var response = controller.GetAll();

            var responseValue = Assert.IsAssignableFrom<IEnumerable<Campaign>>(response.Value);
            Assert.NotEmpty(responseValue);
        }

        [Fact]
        public void Load_ReturnsOK()
        {
            var controller = SetupController();
            var campaign = new Campaign()
            {
                id = 3,
                provider = "Test-Provider-4x4",
                push_message = "Teste j� essa aventura!",
                targeting = new List<Place>()
                {
                    new Place()
                    {
                        place_id = 1,
                        name = "Clube do Teste"
                    },
                    new Place()
                    {
                        place_id = 2,
                        name = "Test Coffee"
                    }
                }
            };

            var response = controller.PostBatch(new List<Campaign>() { campaign });

            Assert.IsAssignableFrom<OkResult>(response);
        }

        //[Fact]
        //public void DeleteAll_ReturnsOK()
        //{
        //    var controller = SetupController();

        //    var response = controller.DeleteAll();

        //    Assert.IsAssignableFrom<OkResult>(response);
        //}
    }
}
