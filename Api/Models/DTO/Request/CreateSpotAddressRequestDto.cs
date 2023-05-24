using Api.Models.Entities;

namespace Api.Models.DTO.Request
{
    public class CreateSpotAddressRequestDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
