using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly ILogger<VisitsController> _logger;

        public VisitsController(ILogger<VisitsController> logger)
        {
            _logger = logger;
        }

        // GET: api/Visits
        [HttpGet]
        public ActionResult<IEnumerable<Visit>> Get()
        {
            throw new NotImplementedException();
        }

        // POST: api/Visits/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Visit> value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Visits/5
        [HttpDelete()]
        public ActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}
