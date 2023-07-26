using Api.Data;
using Api.Models.DTO.Response.ProfileResponse;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using Api.Services.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.ProfileControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserInfoController : ProfileControllerBase
    {
        public UserInfoController(UserManager<ApplicationUser> userManager, AppDbContext context) : base(userManager, context) { }

        [HttpGet]
        [Authorize]
        [Route("Profile")]
        public async Task<IActionResult> Get()
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            var profileInfo = await GetProfileInfo(user);

            return Ok(profileInfo);
        }

        [HttpPut]
        [Authorize]
        [Route("ChangeBio")]
        public async Task<IActionResult> ChangeBio(string bio)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            user.Bio = bio;
            await _context.SaveChangesAsync();

            var profileInfo = await GetProfileInfo(user);

            return Ok(profileInfo);
        }

        private async Task<UserInfoResponse> GetProfileInfo(ApplicationUser user) =>
            await _context.Users
                .Where(u => u.Email == user.Email)
                .Select(u => new UserInfoResponse
                {
                    UserEmail = u.Email,
                    UserName = u.UserName,
                    UserBio = u.Bio,
                    UserPhoto = u.Photos != null ? u.Photos.OrderByDescending(p => p.LastUpdateTime).FirstOrDefault()!.Photo : null,
                    UserSpots = u.Spots
                })
                .FirstOrDefaultAsync();
    }
}
