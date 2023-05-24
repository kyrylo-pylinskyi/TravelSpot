using Api.Models.Entities;

namespace Api.Models.DTO.Request
{
    public class CreateSpotRequestDto
    {
        public string SpotName { get; set; }
        public string SpotDescription { get; set; }
        public SpotCategory? Category { get; set; }
        public SpotRating? Rating { get; set; }
        public List<SpotTag>? Tags { get; set; }
        public List<SpotPhoto>? Photos { get; set; }
        public List<SpotAddress>? Address { get; set; }
    }
}
