using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MormorDagnysInlämning.ViewModels;

namespace MormorDagnysInlämning.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class RolesController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;

    }

    [HttpPost()]
    public async Task<ActionResult> AddRole(RolePostViewModel model) {
        var role = new IdentityRole { Name = model.RoleName, NormalizedName = model.RoleName.ToUpper() };
        await _roleManager.CreateAsync(role);

        return StatusCode(201);
    }
}