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

        // GET: api/Visits/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Visit> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Visits
        [HttpPost]
        public ActionResult Post([FromBody] Visit value)
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

        // PUT: api/Visits/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Visit value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Visits/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
