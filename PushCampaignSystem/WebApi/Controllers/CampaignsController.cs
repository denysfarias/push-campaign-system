using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces(MediaTypeNames.Application.Json)]
    public class CampaignsController : ControllerBase
    {
        private readonly ILogger<CampaignsController> _logger;
        private readonly ICampaignSimpleDataStore _campaignRepository;

        public CampaignsController(ILogger<CampaignsController> logger, ICampaignSimpleDataStore campaignRepository)
        {
            _logger = logger;
            _campaignRepository = campaignRepository;
        }

        // GET: api/Campaigns
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Campaign>> GetAll()
        {
            var result = _campaignRepository.FindAll().ToList();
            return result;
        }

        // POST: api/Campaigns/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Campaign> campaigns)
        {
            _campaignRepository.Load(campaigns);
            return Ok();
        }

        // DELETE: api/Campaigns/
        [HttpDelete()]
        public ActionResult DeleteAll()
        {
            _campaignRepository.Reset();
            return Ok();
        }
    }
}
