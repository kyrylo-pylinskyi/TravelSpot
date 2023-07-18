using Api.Models.Entities.Application;

namespace Api.Models.DTO.Request.Spot
{
    public class SpotRatingRequest
    {
        public Rating Rating { get; set; }
        public int SpotId { get; set; }
    }
}
