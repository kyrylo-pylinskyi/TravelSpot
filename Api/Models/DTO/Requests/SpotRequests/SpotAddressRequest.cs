using Api.Models.Entities;

namespace Api.Models.DTO.Requests.SpotRequests
{
    public class SpotAddressRequest
    {
        public int SpotId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
