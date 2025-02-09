using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ViktorEngmanInlämning.Entities;

namespace ViktorEngmanInlämning.Services;

public class TokenService
{
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        public TokenService(UserManager<User> userManager, IConfiguration config)
        {
                _userManager = userManager;
                _config = config;
        }
        public async Task<string> CreateToken(User user)
        {
                var claims = new List<Claim>{
                        new(ClaimTypes.Email, user.Email),
                        new(ClaimTypes.Name, user.UserName),
                        new("FirstName", user.FirstName),
                        new("LastName", user.LastName)
                };
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                        claims.Add(new(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["tokenSettings:TokenKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var options = new JwtSecurityToken(
                        issuer: null,
                        audience: null,
                        claims: claims,
                        expires: DateTime.Now.AddDays(5),
                        signingCredentials: creds
                );
                return new JwtSecurityTokenHandler().WriteToken(options);
        }
}