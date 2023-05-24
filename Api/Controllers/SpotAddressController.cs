using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.Entities;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
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
        public async Task<IActionResult> CreateSpotAddress([FromForm]CreateSpotAddressRequestDto request)
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

            _context.SpotAddresses.Add(spotAddress);
            _context.SaveChanges();

            return Ok(spotAddress);
        }
    }
}
