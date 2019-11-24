using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Route("[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly ILogger<VisitsController> _logger;

        private readonly IVisitManager _visitManager;

        public VisitsController(IVisitManager visitManager, ILogger<VisitsController> logger)
        {
            _visitManager = visitManager;
            _logger = logger;
        }

        /// <summary>
        /// Gets all visits.
        /// </summary>
        /// <returns>All previous visits</returns>
        // GET: api/Visits
        [HttpGet]
        public ActionResult<IEnumerable<Visit>> GetAll()
        {
            return _visitManager.GetAll().ToList();
        }

        /// <summary>
        /// Loads visits in batch, push associated notifications.
        /// Ids attribution by client.
        /// </summary>
        /// <param name="visits"></param>
        /// <returns>Nothing</returns>
        // POST: api/Visits/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Visit> visits)
        {
            _visitManager.Load(visits);
            return Ok();
        }

        //// DELETE: api/Visits
        //[HttpDelete()]
        //public ActionResult DeleteAll()
        //{
        //    _visitDataStore.Reset();
        //    return Ok();
        //}
    }
}
