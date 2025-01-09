using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MormorDagnysInlämning.Entities;
using MormorDagnysInlämning.Services;
using MormorDagnysInlämning.ViewModels.Account;

namespace MormorDagnysInlämning.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous()]
public class AccountsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly TokenService _tokenService;
    public AccountsController(UserManager<User> userManager, TokenService tokenService)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterUserViewModel model)
    {
        model.UserName = model.Email;
        var user = new User
        {
            UserName = model.UserName,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return StatusCode(500, "error");
        }
        await _userManager.AddToRoleAsync(user, "User");

        return StatusCode(201, "User created");
    }
    [HttpPost("login")]
    public async Task<ActionResult> LoginUser(LoginViewModel model)
    {
        var user = await _userManager.FindByNameAsync(model.UserName);

        if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
        {
            return Unauthorized(new {success=false, message="Unauthorized"});
        }
        return Ok(new {success=true,email = user.Email,token = await _tokenService.CreateToken(user), message="Login successful"});
    }
}
