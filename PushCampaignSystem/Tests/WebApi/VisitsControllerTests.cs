﻿using Microsoft.AspNetCore.Mvc;
using PushNotificationProvider;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.PushCampaignService;
using WebApi.PushCampaignService.DataStore;
using Xunit;

namespace Tests.WebApi
{
    public class VisitsControllerTests
    {
        private VisitsController SetupController(IEnumerable<Visit> visits = null, StringBuilder stringBuilder = null)
        {
            visits = visits ?? new List<Visit>();

            var visitDataStore = new MockVisitStore(visits.Select(model => VisitMapper.ToEntity(model)).ToList());

            var campaigns = CampaignSamples.Get().Select(model => CampaignMapper.ToEntity(model)).ToList();
            var campaignStore = new MockCampaignStore(campaigns);
            var campaignSearch = new SimpleCampaignSearch(campaignStore);
            var stringWriter = new StringWriter(stringBuilder ?? new StringBuilder());

            var pushNotificationProviderFactory = new PushNotificationProviderFactory(stringWriter);

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
            Assert.NotEmpty(responseValue);
        }

        [Fact]
        public void Load_ReturnsOK()
        {
            var stringBuilder = new StringBuilder();
            var controller = SetupController(stringBuilder: stringBuilder);
            var visits = VisitSamples.Get();

            var response = controller.PostBatch(visits);

            Assert.IsAssignableFrom<OkResult>(response);
            var output = stringBuilder.ToString();
            Assert.Contains("Mixpanel", output);
            Assert.Contains("Localytics", output);
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
