using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDagnysInlämning.Data;
using MormorDagnysInlämning.Entities;

namespace MormorDagnysInlämning.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous()]
public class CompanyController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;


    [HttpGet()]
    public async Task<ActionResult> ListAllCompanies()
    {
        var companies = await _context.Salespeople
        .Select(company => new
        {
            company.CompanyName,
            company.SalespersonId,
            company.SalesRep,
            company.Address,
            company.Email,
            company.PhoneNumber
        })
        .ToListAsync();
        return Ok(new { succes = true, StatusCode = 200, data = companies });
    }
    [HttpGet("{Name}")]
    public async Task<ActionResult> GetCompanyByName(string Name)
    {
        var company = await _context.Salespeople
        .Include(Salesperson => Salesperson.Products)
        .Where(Salesperson => Salesperson.CompanyName == Name)
        .Select(company => new
        {
            company.CompanyName,
            company.SalespersonId,
            company.SalesRep,
            company.Address,
            company.Email,
            company.PhoneNumber,
            Products = company.Products.Select(p => new
            {
                p.ProductId,
                p.ItemNumber,
                p.ProductName,
                p.KgPrice,
                p.ImageURL,
                p.Description
            }).ToList()
        })
        .SingleOrDefaultAsync();

        if (company == null)
        {
            return BadRequest (new { succes = false, StatusCode = 400, message = "Company not found" });
        }
        return Ok(new { succes = true, StatusCode = 200, data = company });
    }
    [HttpGet("{Name}/{id}")]
    public async Task<ActionResult> GetProductById(string Name, int id)
    {
        var product = await _context.Salespeople
        .Include(Salesperson => Salesperson.Products)
        .Where(Salesperson => Salesperson.CompanyName == Name)
        .SelectMany(Salesperson => Salesperson.Products)
        .Where(product => product.ProductId == id)
        .Select(p => new
        {
            p.ProductId,
            p.ItemNumber,
            p.ProductName,
            p.KgPrice,
            p.ImageURL,
            p.Description
        })
        .SingleOrDefaultAsync();

        if (product == null)
        {
            return NotFound();
        }
        return Ok(new { succes = true, StatusCode = 200, data = product });
    }

}