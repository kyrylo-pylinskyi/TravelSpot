using Api.Data;
using Api.Models.DTO.Request.Spot;
using Api.Models.Entities.Application;
using Api.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.SpotControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotAddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SpotAddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpotAddress([FromForm] SpotAddressRequest request)
        {
            var address = await GeoLocationService.GetAddress(request.Latitude, request.Longitude);
            var spotAddress = new SpotAddress
            {
                PlaceId = address["PlaceId"],
                OsmId = address["OsmId"],
                Country = address["Country"],
                State = address["State"],
                City = address["City"],
                Street = address["Street"],
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                SpotId = request.SpotId
            };

            await _context.SpotAddresses.AddAsync(spotAddress);
            await _context.SaveChangesAsync();

            return Ok(spotAddress);
        }
    }
}
