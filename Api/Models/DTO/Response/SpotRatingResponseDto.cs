using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response
{
    public class SpotRatingResponseDto
    {
        public int Id { get; set; }
        public int SpotId { get; set; }
        public double Rating { get; set; }

        public SpotRatingResponseDto(SpotRating spotRating)
        {
            Id = spotRating.Id;
            SpotId = spotRating.SpotId;
            Rating = (double)spotRating.Rating;
        }
        
        public static IEnumerable<SpotRatingResponseDto> CreateResponse(List<SpotRating> spotRatings)
        {
            foreach (var item in spotRatings)
            {
                yield return new SpotRatingResponseDto(item);
            }
        }
    }
}
