using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SpotController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpot([FromForm]SpotRequestDto request)
        {
            Spot spot = new Spot
            {
                Name = request.SpotName,
                Description = request.SpotDescription,
            };

            await _context.Spots.AddAsync(spot);
            await _context.SaveChangesAsync();
            return Ok(new {SpotId = spot.Id, SpotName = spot.Name, SpotDescription = spot.Description});
        }

        [HttpGet]
        public async Task<IActionResult> GetSpots()
        {
            var spots = await _context.Spots
                                .Include(s => s.Addresses)
                                .Include(s => s.Photos)
                                .Select(s => new SpotResponseDto
                                {
                                    Id = s.Id,
                                    Name = s.Name,
                                    Description = s.Description,
                                    Addresses = SpotAddressResponseDto.CreateResponse(s.Addresses).ToList(),
                                    Photos = SpotPhotoResponseDto.CreateResponse(s.Photos).ToList(),
                                }).ToListAsync();
            return Ok(spots);
        }
    }
}