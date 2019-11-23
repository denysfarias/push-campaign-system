using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
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

        // GET: api/Campaigns
        [HttpGet]
        public ActionResult<IEnumerable<Campaign>> GetAll()
        {
            var result = _campaignManager.GetAll().ToList();
            return result;
        }

        // POST: api/Campaigns/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Campaign> campaigns)
        {
            _campaignManager.Load(campaigns);
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
