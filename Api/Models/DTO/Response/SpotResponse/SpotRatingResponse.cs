using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotRatingResponse
    {
        public double Rating { get; set; }

        public SpotRatingResponse(SpotRate spotRating)
        {
            Rating = (double)spotRating.Rating;
        }

        public static IEnumerable<SpotRatingResponse> CreateResponse(List<SpotRate> spotRatings)
        {
            foreach (var item in spotRatings)
            {
                yield return new SpotRatingResponse(item);
            }
        }
    }
}
