using Domain.Notifications.DataTransferObjects;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

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
            var result = _visitManager.GetAll();

            if (result.IsInvalid)
                return new StatusCodeResult(500);

            return result.Object
                .Select(entity => VisitMapper.ToModel(entity))
                .ToList();
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
            var validations = visits.Select(campaign => campaign.Validate()).ToArray();
            var summaryValidation = new CommandNotification(validations);
            if (summaryValidation.IsInvalid)
                return StatusCode(500, summaryValidation.Notifications);

            var entities = visits.Select(model => VisitMapper.ToEntity(model)).ToList();
            var result = _visitManager.Load(entities);

            if (result.IsInvalid)
                return StatusCode(500, result.Notifications);

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
