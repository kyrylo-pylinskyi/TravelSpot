using Api.Services.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AtractionControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityAtractionController : ControllerBase
    {
        [HttpGet("{city}")]
        public async Task<IActionResult> Get(string city)
        {
            return Ok(await CityAtractionsService.GetCityAtractions(city));
        }
    }
}
