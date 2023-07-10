using Api.Data;
using Api.Models.DTO.Request.Login;
using Api.Services.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return BadRequest("user not found");
            if (!Credentials.Verify(request.Password, user.PasswordHash, user.PasswordSalt))
                return BadRequest("password is not correct");

            return Ok(Credentials.CreateJwt(user));
        }
    }
}
