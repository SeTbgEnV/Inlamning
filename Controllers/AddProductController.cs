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
                .Where(Salesperson => Salesperson.SalespersonId == id)
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
                SalesRepId = supplier.SalespersonId,
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
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.ProductId }, new { success = true, StatusCode = 201, data = newProduct });
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

        [HttpGet("name/{ProductName}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductByName(string ProductName)
        {
            var product = await _context.Products
                .Where(p => p.ProductName == ProductName)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound(new { success = false, StatusCode = 404, message = "Product not found" });
            }

            var salespersons = await _context.Salespeople
                .Include(s => s.Products)
                .Where(s => s.Products.Any(p => p.ProductName == ProductName))
                .Select(s => new
                {
                    s.SalespersonId,
                    s.SalesRep,
                    s.CompanyName,
                    s.Address,
                    s.Email,
                    s.PhoneNumber,
                    Products = s.Products.Where(p => p.ProductName == ProductName).ToList()
                })
                .ToListAsync();

            return Ok(new { success = true, StatusCode = 200, product, salespersons });
        }


        [HttpPatch("{id}")]
        // [Authorize(Roles = "SalesSupport, Admin")]
        public async Task<ActionResult> UpdatePrice(int id, ProductPriceViewModel product)
        {
            var productToUpdate = await _context.Products
                .Where(product => product.ProductId == id)
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
                .Where(Product => Product.ProductId == id)
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