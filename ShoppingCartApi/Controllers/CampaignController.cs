using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using ShoppingCartApi.Models;
using ShoppingCartApi.Repositories;
using ShoppingCartApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CampaignController : Controller
    {
        private readonly ICampaignRepository _campaignRepository;
    
        public CampaignController(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        // GET api/product
        // example: http://localhost:5000/api/campaign
        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Campaign>> Get()
        {
            return await _campaignRepository.Get();
        }

        // GET api/product/id
        // example: http://localhost:5000/api/campaign49902812
        [HttpGet("{id}")]
        public async Task<Campaign> Get(string id)
        {
            return await _campaignRepository.Get(id) ?? new Campaign();
        }

        // POST api/product
        // example: http://localhost:5000/api/campaign
        [HttpPost]
        public void Post([FromBody] Campaign campaign)
        {
            _campaignRepository.Create(campaign);
        }

        // PUT api/product
        // example: http://localhost:5000/api/campaign
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Campaign campaignIn)
        {
            var campaign = _campaignRepository.Get(id);

            if (campaign == null)
            {
                return NotFound();
            } 

            _campaignRepository.Update(id, campaignIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var campaign = _campaignRepository.Get(id);

            if (campaign == null)
            {
                return NotFound();
            }

            _campaignRepository.Remove(id);

            return NoContent();
        }

    }
}