using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.PushCampaignService;
using WebApi.PushCampaignService.DataStore;
using WebApi.PushNotificationProviders;
using Xunit;

namespace Tests.WebApi
{
    public class VisitsControllerTests
    {
        private VisitsController SetupController(IEnumerable<Visit> visits = null)
        {
            var visitDataStore = new MockVisitStore(visits ?? new List<Visit>());

            var campaigns = CampaignSamples.Get();
            var campaignStore = new MockCampaignStore(campaigns);
            var campaignSearch = new SimpleCampaignSearch(campaignStore);

            var pushNotificationProviderFactory = new PushNotificationProviderFactory();

            var manager = new VisitManager(visitDataStore, campaignSearch, pushNotificationProviderFactory);

            return new VisitsController(manager, logger: null);
        }

        [Fact]
        public void GetAll_ReturnsVisits()
        {
            var visits = VisitSamples.Get();
            var controller = SetupController(visits);

            var response = controller.GetAll();

            var responseValue = Assert.IsAssignableFrom<IEnumerable<Visit>>(response.Value);
            Assert.NotNull(responseValue);
        }

        [Fact]
        public void Load_ReturnsOK()
        {
            var controller = SetupController();
            var visits = VisitSamples.Get();

            var response = controller.PostBatch(visits);

            Assert.IsAssignableFrom<OkResult>(response);
        }

        //[Fact]
        //public void DeleteAll_ReturnsOK()
        //{
        //    var visits = VisitSamples.Get();
        //    var controller = SetupController(visits);
            
        //    var response = controller.DeleteAll();

        //    Assert.IsAssignableFrom<OkResult>(response);
        //}
    }
}
