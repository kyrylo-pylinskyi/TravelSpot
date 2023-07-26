using Api.Data;
using Api.Models.DTO.Requests.ProfileRequests;
using Api.Models.DTO.Response.ProfileResponse;
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
    public class UserPhotoController : ProfileControllerBase
    {
        public UserPhotoController(UserManager<ApplicationUser> userManager, AppDbContext context) : base(userManager, context) { }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            var profilePhotos = GetUserPhotos(user);

            return Ok(profilePhotos);
        }

        [HttpGet]
        [Authorize]
        [Route("Main")]
        public async Task<IActionResult> GetMain()
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            var profilePhotos = await GetUserPhotos(user);

            return Ok(profilePhotos.FirstOrDefault());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] UserPhotoRequest request)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            var photo = new UserPhoto
            {
                UserId = user.Id,
                LastUpdateTime = DateTime.Now,
                Photo = request.Photo.ToByteArray()
            };

            await _context.UserPhotos.AddAsync(photo);
            await _context.SaveChangesAsync();

            var profilePhotos = await GetUserPhotos(user);

            return Ok(profilePhotos);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            var photo = await _context.UserPhotos.FirstOrDefaultAsync(p => p.Id == id && p.UserId == user.Id);
            if (photo == null)
                return BadRequest(new { msg = "Photo not found", id });

            _context.UserPhotos.Remove(photo);
            await _context.SaveChangesAsync();

            var profilePhotos = await GetUserPhotos(user);

            return Ok(profilePhotos);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> SetAsMain(int id)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return BadRequest(new { msg = "User not found" });

            var photo = await _context.UserPhotos.FirstOrDefaultAsync(p => p.Id == id && p.UserId == user.Id);
            if (photo == null)
                return BadRequest(new { msg = "Photo not found", id });

            photo.LastUpdateTime = DateTime.Now;
            await _context.SaveChangesAsync();

            var profilePhotos = await GetUserPhotos(user);

            return Ok(profilePhotos);
        }

        private async Task<List<UserPhotoResponse>> GetUserPhotos(ApplicationUser user) =>
            await _context.Users.Include(u => u.Photos)
                                .Where(u => u.Email == user.Email)
                                .SelectMany(u => u.Photos.Select(p => new UserPhotoResponse
                                {
                                    UserId = u.Id,
                                    PhotoId = p.Id,
                                    LastUpdateTime = p.LastUpdateTime,
                                    Photo = p.Photo,
                                })).OrderByDescending(p => p.LastUpdateTime).ToListAsync();
    }
}
