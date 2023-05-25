using Api.Models.Entities;

namespace Api.Models.DTO.Request
{
    public class SpotRatingRequestDto
    {
        public Rating Rating { get; set; }
        public int SpotId { get; set; }
    }
}
