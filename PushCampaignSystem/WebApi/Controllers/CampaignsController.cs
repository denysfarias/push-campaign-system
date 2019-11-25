using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly ICampaignManager _campaignManager;

        public CampaignsController(ICampaignManager campaignManager, ILogger<CampaignsController> logger)
        {
            _campaignManager = campaignManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets all campaigns.
        /// </summary>
        /// <returns>All active campaigns</returns>
        // GET: api/Campaigns
        [HttpGet]
        public ActionResult<IEnumerable<Campaign>> GetAll()
        {
            var result = _campaignManager.GetAll();

            if (result.IsInvalid)
                return new StatusCodeResult(500);

            return result.Object
                .Select(entity => CampaignMapper.ToModel(entity))
                .ToList();
        }

        /// <summary>
        /// Loads campaigns in batch. 
        /// Ids attribution by client.
        /// </summary>
        /// <param name="campaigns"></param>
        /// <returns>Nothing</returns>
        // POST: api/Campaigns/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Campaign> campaigns)
        {
            var entities = campaigns.Select(model => CampaignMapper.ToEntity(model)).ToList();
            var result =_campaignManager.Load(entities);

            if (result.IsInvalid)
                return new StatusCodeResult(500);

            return Ok();
        }

        //// DELETE: api/Campaigns/
        //[HttpDelete()]
        //public ActionResult DeleteAll()
        //{
        //    _campaignDataStore.Reset();
        //    return Ok();
        //}
    }
}
