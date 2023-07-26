using Api.Data;
using Api.Models.DTO.Requests.SpotRequests;
using Api.Models.DTO.Response.SpotResponse;
using Api.Models.Entities.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.SpotControllers
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
        public async Task<IActionResult> CreateSpot([FromForm] SpotRequest request)
        {
            Spot spot = new Spot
            {
                Name = request.SpotName,
                Description = request.SpotDescription,
            };

            await _context.Spots.AddAsync(spot);
            await _context.SaveChangesAsync();
            return Ok(new { SpotId = spot.Id, SpotName = spot.Name, SpotDescription = spot.Description });
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
                                .Select(s => new SpotResponse
                                {
                                    Id = s.Spot.Id,
                                    Name = s.Spot.Name,
                                    Description = s.Spot.Description,
                                    Tags = SpotTagResponse.CreateResponse(s.Spot.Tags).ToList(),
                                    Category = SpotCategoryResponse.CreateResponse(s.Category),
                                    Addresses = SpotAddressResponse.CreateResponse(s.Spot.Addresses).ToList(),
                                    Ratings = SpotRatingResponse.CreateResponse(s.Spot.Ratings).ToList(),
                                    Photos = SpotPhotoResponse.CreateResponse(s.Spot.Photos).ToList(),
                                }).ToListAsync();
            return Ok(spots);
        }
    }
}