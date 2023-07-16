using Api.Models.DTO.Request.Authorization.Login;
using Api.Models.Entities.Identity;
using Api.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityLoginController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthOptions _authOptions;

        public IdentityLoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<AuthOptions> authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions.Value;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail) ??
               await _userManager.FindByNameAsync(request.UserNameOrEmail);


            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_authOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }
    }
}
