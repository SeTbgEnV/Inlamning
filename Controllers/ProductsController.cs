using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViktorEngmanInlämning.Data;
using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.ViewModels;

namespace MormorDagnysInlämning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductsRepository repo) : ControllerBase
    {
        private readonly DataContext _context = context;
        private readonly IProductsRepository _repo = repo;
        [HttpGet]
        public async Task<ActionResult> Get()
        {

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(product);
        }

        [HttpPost("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> Add(int id, ProductViewModel product)
        {
            return Ok(product);
        }

        [HttpPatch("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> UpdatePrice(int id, ProductPriceViewModel product)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}