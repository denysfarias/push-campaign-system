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
        private readonly ICampaignRepository _campaignRepository;

        public CampaignsController(ILogger<CampaignsController> logger, ICampaignRepository campaignRepository)
        {
            _logger = logger;
            _campaignRepository = campaignRepository;
        }

        // GET: api/Campaigns
        [HttpGet]
        [Route("batch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Campaign>> GetAll()
        {
            var result = _campaignRepository.FindAll().ToList();
            return result;
        }

        // GET: api/Campaigns/5
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Campaign> GetById(int id)
        {
            var result = _campaignRepository.FindById(id);
            
            if (result == null)
                return NotFound();

            return result;
        }

        // POST: api/Campaigns
        [HttpPost]
        public ActionResult Post([FromBody] Campaign value)
        {
            throw new NotImplementedException();

            var result = _campaignRepository.Create(value);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, null);
        }

        // POST: api/Campaigns/batch/
        [HttpPost]
        [Route("batch")]
        public ActionResult PostBatch([FromBody] IEnumerable<Campaign> value)
        {
            throw new NotImplementedException();

            var ids = new List<int>(capacity: value.Count());
            foreach (var campaign in value)
            {
                ids.Add(_campaignRepository.Create(campaign).Id);
            }
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
