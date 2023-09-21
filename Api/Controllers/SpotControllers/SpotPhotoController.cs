using Api.Data;
using Api.Models.DTO.Requests.SpotRequests;
using Api.Models.DTO.Response.SpotResponse;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using Api.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.SpotControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotPhotoController : ApplicationControllerBase
    {
        public SpotPhotoController(UserManager<ApplicationUser> userManager, AppDbContext context) : base(userManager, context)
        {
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSpotPhoto([FromForm] SpotPhotoRequest request)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return Unauthorized(new { msg = "Unautorized" });

            var spot = await _context.Spots.FindAsync(request.SpotId);

            if (user.Id != spot.AuthorId)
                return BadRequest(new { msg = "You can not append photo to this spot", request.SpotId, spot.AuthorId, UserId = user.Id });

            var spotPhoto = new SpotPhoto
            {
                SpotId = request.SpotId,
                Photo = request.SpotPhoto.ToByteArray()
            };

            await _context.SpotPhotos.AddAsync(spotPhoto);
            await _context.SaveChangesAsync();

            return Ok(await SelectSpotPhotos(request.SpotId));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSpotPhoto([FromForm] SpotPhotoUpdateRequest request)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return Unauthorized(new { msg = "Unautorized" });

            var spotPhoto = await _context.SpotPhotos.FindAsync(request.SpotPhotoId);

            if (spotPhoto == null)
                return BadRequest(new { msg = "Photo not found", request.SpotPhotoId });

            var spot = await _context.Spots.FindAsync(spotPhoto.SpotId);

            if (user.Id != spot.AuthorId)
                return BadRequest(new { msg = "You can not edit this photo", request.SpotPhotoId, spot.AuthorId, UserId = user.Id });

            spotPhoto.Photo = request.SpotPhoto.ToByteArray();

            await _context.SaveChangesAsync();

            return Ok(await SelectSpotPhotos(spotPhoto.SpotId));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteSpotPhoto(int id)
        {
            var user = await GetAuthorizedUser();
            if (user == null)
                return Unauthorized(new { msg = "Unautorized" });

            var spotPhoto = await _context.SpotPhotos.FindAsync(id);
            if (spotPhoto == null)
                return BadRequest(new { msg = "Photo not found", SpotPhotoId = id });

            var spot = await _context.Spots.FindAsync(spotPhoto.SpotId);

            if (user.Id != spot.AuthorId)
                return BadRequest(new { msg = "You can not edit this photo", SpotPhotoId = id, spot.AuthorId, UserId = user.Id });

            _context.SpotPhotos.Remove(spotPhoto);
            await _context.SaveChangesAsync();

            return Ok(await SelectSpotPhotos(spotPhoto.SpotId));
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetSpotPhotos(int id)
        {
            return Ok(await SelectSpotPhotos(id));
        }

        private async Task<List<SpotPhotoResponse>> SelectSpotPhotos(int spotId) 
        {
            var spotPhotos = await _context.SpotPhotos.Where(sp => sp.SpotId == spotId).ToListAsync();
            return SpotPhotoResponse.CreateResponse(spotPhotos).ToList();
        }
    }
}
