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
                                .Include(s => s.Ratings)
                                .Include(s => s.Tags)
                                .ThenInclude(t => t.Tag)
                                .Join(_context.SpotCategories, 
                                        s => s.Id, sc => sc.SpotId,
                                        (s, sc) => new
                                        {
                                            Spot = s,
                                            Category = sc
                                        })
                                .Select(s => new SpotResponseDto
                                {
                                    Id = s.Spot.Id,
                                    Name = s.Spot.Name,
                                    Description = s.Spot.Description,
                                    Tags = SpotTagResponseDto.CreateResponse(s.Spot.Tags).ToList(),
                                    Category = SpotCategoryResponseDto.CreateResponse(s.Category),
                                    Addresses = SpotAddressResponseDto.CreateResponse(s.Spot.Addresses).ToList(),
                                    Ratings = SpotRatingResponseDto.CreateResponse(s.Spot.Ratings).ToList(),
                                    Photos = SpotPhotoResponseDto.CreateResponse(s.Spot.Photos).ToList(),
                                }).ToListAsync();
            return Ok(spots);
        }
    }
}