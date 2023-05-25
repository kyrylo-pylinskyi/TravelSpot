using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotRatingController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SpotRatingController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpotRating([FromForm]SpotRatingRequestDto request)
        {
            var spotRating = new SpotRating
            {
                SpotId = request.SpotId,
                Rating = request.Rating,
            };

            await _context.SpotRatings.AddAsync(spotRating);
            await _context.SaveChangesAsync();

            return Ok(spotRating);
        }
    }
}
