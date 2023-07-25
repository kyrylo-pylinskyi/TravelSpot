using Api.Data;
using Api.Models.DTO.Request.Profile;
using Api.Models.DTO.Response.Profile;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using Api.Services.Helpers;
using Api.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers.ProfileControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserPhotoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public UserPhotoController(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            string email = UserService.GetEmail(HttpContext);
            if (string.IsNullOrEmpty(email))
                return NotFound(new { msg = "Email is null or not found" });

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { msg = "User not found" });

            var photos = GetUserPhotos(user.Id);

            return Ok(photos);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromForm] UserPhotoRequest request)
        {
            string email = UserService.GetEmail(HttpContext);
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { msg = "Email is null or not found" });

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { msg = "User not found" });

            var photo = new UserPhoto
            {
                UserId = user.Id,
                LastUpdateTime = DateTime.Now,
                Photo = request.Photo.ToByteArray()
            };

            await _context.UserPhotos.AddAsync(photo);
            await _context.SaveChangesAsync();

            var photos = GetUserPhotos(user.Id);

            return Ok(new { msg = "OK", photos });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            string email = UserService.GetEmail(HttpContext);
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { msg = "Email is null or not found" });

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { msg = "User not found" });

            var photo = await _context.UserPhotos.FindAsync(id);

            _context.UserPhotos.Remove(photo);

            await _context.SaveChangesAsync();

            var photos = GetUserPhotos(user.Id);

            return Ok(new { msg = "OK", photos });
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> SetAsMain(int id)
        {
            string email = UserService.GetEmail(HttpContext);
            if (string.IsNullOrEmpty(email))
                return BadRequest(new { msg = "Email is null or not found", email });

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(new { msg = "User not found", email });

            var photo = await _context.UserPhotos.FindAsync(id);
            if (photo == null)
                return BadRequest(new { msg = "Photo not found", id });

            photo.LastUpdateTime = DateTime.Now;
            await _context.SaveChangesAsync();

            var photos = GetUserPhotos(user.Id);

            return Ok(new { msg = "OK", photos });
        }

        private List<UserPhotoResponse> GetUserPhotos(string userId) =>
            _context.UserPhotos.Where(up => up.UserId == userId)
                                 .Select(p => new UserPhotoResponse()
                                 {
                                     UserId = p.UserId,
                                     LastUpdateTime = p.LastUpdateTime,
                                     Photo = p.Photo
                                 })
                                 .OrderByDescending(p => p.LastUpdateTime)
                                 .ToList();
    }
}
