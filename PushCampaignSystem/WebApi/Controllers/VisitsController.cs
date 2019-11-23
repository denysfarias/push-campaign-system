using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly ILogger<VisitsController> _logger;

        private readonly ISimpleDataStore<Campaign> _campaignDataStore;

        private readonly ISimpleDataStore<Visit> _visitDataStore;

        public VisitsController(ILogger<VisitsController> logger, ISimpleDataStore<Campaign> campaignDataStore, ISimpleDataStore<Visit> visitDataStore)
        {
            _logger = logger;
            _campaignDataStore = campaignDataStore;
            _visitDataStore = visitDataStore;
        }

        // GET: api/Visits
        [HttpGet]
        public ActionResult<IEnumerable<Visit>> GetAll()
        {
            return _visitDataStore.FindAll().ToList();
        }

        // POST: api/Visits/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Visit> visits)
        {
            _visitDataStore.Load(visits);
            return Ok();
        }

        // DELETE: api/Visits
        [HttpDelete()]
        public ActionResult DeleteAll()
        {
            _visitDataStore.Reset();
            return Ok();
        }
    }
}
