using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.Entities.Application;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromForm] TagRequestDto request)
        {
            var tag = new Tag
            {
                Name = request.Name,
                Description = request.Description,
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return Ok(tag);
        }

        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            return Ok(_context.Tags); 
        }
    }
}
