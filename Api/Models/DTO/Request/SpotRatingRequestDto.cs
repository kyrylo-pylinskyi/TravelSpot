using Api.Models.Entities.Application;

namespace Api.Models.DTO.Request
{
    public class SpotRatingRequestDto
    {
        public Rating Rating { get; set; }
        public int SpotId { get; set; }
    }
}
