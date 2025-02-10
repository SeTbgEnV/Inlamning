using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViktorEngmanInlämning.Data;
using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.Interfaces;
using ViktorEngmanInlämning.ViewModels.Supplier;

namespace MormorDagnysInlämning.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous()]
public class CompanyController(ISupplierRepository repo) : ControllerBase
{
    private readonly ISupplierRepository _repo = repo;

    [HttpGet()]
    public async Task<IActionResult> ListAllSuppliers()
    {
        try
        {
            // Steg 3.
            return Ok(new { success = true, data = await _repo.List() });
        }
        catch (Exception ex)
        {
            return NotFound($"Tyvärr hittade vi inget {ex.Message}");
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSupplier(int id)
    {
        // Steg 3.
        try
        {
            var supplier = await _repo.GetSupplier(id);
            return Ok(new { success = true, data = await _repo.GetSupplier(id) });
        }
        catch (Exception ex)
        {
            return NotFound(new { success = false, message = ex.Message });
        }
    }
    
    [HttpPost()]
    public async Task<IActionResult> Add(SupplierPostViewModel model)
    {
        if (await _repo.Add(model))
        {
            return StatusCode(201);
        }
        else
        {
            return BadRequest();
        }
    }

}