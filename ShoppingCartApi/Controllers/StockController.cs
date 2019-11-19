using System.Threading.Tasks;
using System.Collections.Generic;
using ShoppingCartApi.Models;
using ShoppingCartApi.Repositories;
using ShoppingCartApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
    
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
       

        // GET api/stock
        // example: http://localhost:5000/api/stock
        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Stock>> Get()
        {
            return await _stockRepository.Get();
        }

        // GET api/stock/id
        // example: http://localhost:5000/api/stock/49902812
        [HttpGet("{id}")]
        public async Task<Stock> Get(string id)
        {
            return await _stockRepository.Get(id) ?? new Stock();
        }

        // POST api/stock
        // example: http://localhost:5000/api/stock
        [HttpPost]
        public void Post([FromBody] Stock stock)
        {
            _stockRepository.Create(stock);
        }

        // PUT api/stock
        // example: http://localhost:5000/api/stock
        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, [FromBody] Stock stockIn)
        {
            var stock = _stockRepository.Get(id);

            if (stock == null)
            {
                return NotFound();
            } 

            _stockRepository.Update(id, stockIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var stock = _stockRepository.Get(id);

            if (stock == null)
            {
                return NotFound();
            }

            _stockRepository.Remove(id);

            return NoContent();
        }

    }
}