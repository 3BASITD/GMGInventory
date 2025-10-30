using GMGInventory.Common.Models;
using GMGInventory.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GMGInventory.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
        }


        //[HttpGet("ping")]
        //public IActionResult Ping() => Ok("API is alive");

        [HttpPost("Token")]
        public async Task<IActionResult> GetToken([FromBody] APILogin login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null) return Unauthorized("Invalid username or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) return Unauthorized("Invalid username or password");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Authority"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            //return Ok(new
            //{
            //    token = new JwtSecurityTokenHandler().WriteToken(token),
            //    expiration = token.ValidTo
            //});
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
