using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotTagController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SpotTagController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpotTag([FromForm] SpotTagRequestDto request)
        {
            var spotTag = new SpotTag
            {
                SpotId = request.SpotId,
                TagId = request.TagId,
            };

            await _context.SpotTags.AddAsync(spotTag);
            await _context.SaveChangesAsync();

            return Ok(spotTag);
        }
    }
}
