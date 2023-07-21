using Api.Data;
using Api.Models.Entities.Identity;
using Api.Services.Helpers;
using Api.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers.ProfileControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UesrPhotoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public UesrPhotoController(UserManager<ApplicationUser> userManager, AppDbContext context) //, IUserService userService)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            ApplicationUser user = null;
            if (identity != null)
            {
                string email = identity.FindFirst(ClaimTypes.Email).Value;

                user = await _userManager.FindByEmailAsync(email);
            }

            return Ok(_context.UserPhotos.Where(up => up.UserId == user.Id));
        }
    }
}
