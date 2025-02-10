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
    public class AddProductController(DataContext context) : ControllerBase
    {
        private readonly DataContext _context = context;

        [HttpPost("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> AddProductToSupplier(int id, ProductViewModel product)
        {
            var supplier = await _context.Salespeople
                .Where(supplier => supplier.Id == id)
                .SingleOrDefaultAsync();

            if (supplier == null)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Supplier not found" });
            }
            var existingProduct = await _context.Products
                .Where(Product => Product.ItemNumber == product.ItemNumber)
                .SingleOrDefaultAsync();
            if (existingProduct != null)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Product already exists" });
            }

            var newProduct = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                KgPrice = product.Price,
                SalesRepId = supplier.Id,
                ImageURL = product.ImageURL,
                ItemNumber = product.ItemNumber
            };

            _context.Products.Add(newProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Product already exists" });
            }
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, new { success = true, StatusCode = 201, data = newProduct });
        }

        [HttpGet("{id}")]
        [AllowAnonymous()]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = "Product not found" });
            }

            return Ok(product);
        }

        [HttpPatch("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> UpdatePrice(int id, ProductPriceViewModel product)
        {
            var productToUpdate = await _context.Products
                .Where(product => product.Id == id)
                .SingleOrDefaultAsync();

            if (productToUpdate == null)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Product not found" });
            }
            productToUpdate.KgPrice = product.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Product not found" });
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products
                .Where(Product => Product.Id == id)
                .SingleOrDefaultAsync();

            if (productToDelete == null)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Product not found" });
            }
            _context.Products.Remove(productToDelete);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { success = false, StatusCode = 400, message = "Product not found" });
            }

            return NoContent();
        }
    }
}