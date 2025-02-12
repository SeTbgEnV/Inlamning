using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViktorEngmanInlämning.Repositories;

public class ProductRepository(DataContext context) : IProductRepository
{
    private readonly DataContext _context = context;
    private readonly IAddressRepository _repo = repo;
    public async Task<IEnumerable<Product>> Get()
    {
        var products = await _repo.Get();
    }
    public async Task<Product> Get(int id)
    {
        var product = await _repo.Get(id);
        if (product == null)
        {
            return NotFound(new { success = false, StatusCode = 404, message = "Product not found" });
        }
    }
    public async Task<Product> Add(Product product)
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
    }
    public async Task<Product> UpdatePrice(Product product)
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
    }
    public async Task<Product> Delete(int id)
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
    }
}
