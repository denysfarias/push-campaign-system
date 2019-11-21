using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignsController : ControllerBase
    {
        private readonly ILogger<CampaignsController> _logger;

        public CampaignsController(ILogger<CampaignsController> logger)
        {
            _logger = logger;
        }

        // GET: api/Campaigns
        [HttpGet]
        public ActionResult<IEnumerable<Campaign>> Get()
        {
            throw new NotImplementedException();
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Campaign> Get(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/Campaigns
        [HttpPost]
        public ActionResult Post([FromBody] Campaign value)
        {
            throw new NotImplementedException();
        }

        // POST: api/Campaigns/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Campaign> value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Campaigns/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Campaign value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Campaigns/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
