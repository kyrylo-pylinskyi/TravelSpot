using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.Entities.Application;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
        public async Task<IActionResult> CreateSpotPhoto([FromForm] SpotPhotoRequestDto request)
        {
            var spotPhoto = new SpotPhoto
            {
                SpotId = request.SpotId,
                Photo = PhotoService.FormFileToByteArray(request.SpotPhoto)
            };

            await _context.SpotPhotos.AddAsync(spotPhoto);
            await _context.SaveChangesAsync();

            return Ok(spotPhoto);
        }
    }
}
