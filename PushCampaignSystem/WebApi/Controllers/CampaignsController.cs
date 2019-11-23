using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly ISimpleDataStore<Campaign> _campaignDataStore;

        public CampaignsController(ILogger<CampaignsController> logger, ISimpleDataStore<Campaign> campaignDataStore)
        {
            _logger = logger;
            _campaignDataStore = campaignDataStore;
        }

        // GET: api/Campaigns
        [HttpGet]
        public ActionResult<IEnumerable<Campaign>> GetAll()
        {
            var result = _campaignDataStore.FindAll().ToList();
            return result;
        }

        // POST: api/Campaigns/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Campaign> campaigns)
        {
            _campaignDataStore.Load(campaigns);
            return Ok();
        }

        // DELETE: api/Campaigns/
        [HttpDelete()]
        public ActionResult DeleteAll()
        {
            _campaignDataStore.Reset();
            return Ok();
        }
    }
}
