using Api.Data;
using Api.Models.DTO.Requests.SpotRequests;
using Api.Models.DTO.Response.SpotResponse;
using Api.Models.Entities.Application;
using Api.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.SpotControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotController : ApplicationControllerBase
    {
        public SpotController(UserManager<ApplicationUser> userManager, AppDbContext context) : base(userManager, context)
        {
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateSpot([FromForm] SpotRequest request)
        {
            var user = await GetAuthorizedUser();

            Spot spot = new Spot
            {
                AuthorId = user.Id,
                Name = request.SpotName,
                Description = request.SpotDescription,
            };

            await _context.Spots.AddAsync(spot);
            await _context.SaveChangesAsync();
            return Ok(new { SpotId = spot.Id, SpotName = spot.Name, SpotDescription = spot.Description });
        }

        [HttpDelete("Delete/{spotId}")]
        public async Task<IActionResult> DeleteSpot(int spotId)
        {
            var user = await GetAuthorizedUser();

            var spot = await _context.Spots.FindAsync(spotId);

            if (spot == null)
                return BadRequest(new { msg = "Spot not found", spotId });

            if (spot.AuthorId != user.Id)
                return BadRequest(new { msg = "You can not delete this Spot", UserId = user.Id, spot.AuthorId });

            _context.Spots.Remove(spot);
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Spot Deleted", SpotId = spot.Id, SpotName = spot.Name, SpotDescription = spot.Description });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSpot([FromForm] SpotUpdateRequest request)
        {
            var user = await GetAuthorizedUser();

            var spot = await _context.Spots.FindAsync(request.SpotId);

            if (spot == null)
                return BadRequest(new { msg = "Spot not found", request.SpotId });

            if (spot.AuthorId != user.Id)
                return BadRequest(new { msg = "You can not edit this Spot", UserId = user.Id, spot.AuthorId });

            spot.Name = request.SpotName;
            spot.Description = request.SpotDescription;
            await _context.SaveChangesAsync();
            return Ok(new { msg = "Spot Updated", SpotId = spot.Id, SpotName = spot.Name, SpotDescription = spot.Description });
        }

        [HttpGet]
        public async Task<IActionResult> GetSpots()
        {
            var spots = await _context.Spots
                                .Include(s => s.Author)
                                
                                .Include(s => s.Addresses)
                                .Include(s => s.Photos)
                                .Include(s => s.Rates)
                                .Include(s => s.Tags)
                                .ThenInclude(t => t.Tag)
                                .ToListAsync();
            return Ok(spots);
        }
    }
}