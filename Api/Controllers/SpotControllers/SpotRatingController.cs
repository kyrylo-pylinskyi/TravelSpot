using Api.Data;
using Api.Models.DTO.Requests.SpotRequests;
using Api.Models.Entities.Application;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SpotControllers
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
        public async Task<IActionResult> CreateSpotRating([FromForm] SpotRatingRequest request)
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
