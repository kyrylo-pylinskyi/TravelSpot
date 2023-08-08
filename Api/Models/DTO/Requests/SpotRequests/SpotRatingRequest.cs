using Api.Models.Entities.Application;

namespace Api.Models.DTO.Requests.SpotRequests
{
    public class SpotRatingRequest
    {
        public Rate Rating { get; set; }
        public int SpotId { get; set; }
    }
}
