using Api.Data;
using Api.Models.DTO.Request.Spot;
using Api.Models.Entities.Application;
using Api.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SpotControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotPhotoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SpotPhotoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpotPhoto([FromForm] SpotPhotoRequest request)
        {
            var spotPhoto = new SpotPhoto
            {
                SpotId = request.SpotId,
                Photo = request.SpotPhoto.ToByteArray()
            };

            await _context.SpotPhotos.AddAsync(spotPhoto);
            await _context.SaveChangesAsync();

            return Ok(spotPhoto);
        }
    }
}
