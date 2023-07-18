using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response.Spot
{
    public class SpotRatingResponse
    {
        public int Id { get; set; }
        public int SpotId { get; set; }
        public double Rating { get; set; }

        public SpotRatingResponse(SpotRating spotRating)
        {
            Id = spotRating.Id;
            SpotId = spotRating.SpotId;
            Rating = (double)spotRating.Rating;
        }

        public static IEnumerable<SpotRatingResponse> CreateResponse(List<SpotRating> spotRatings)
        {
            foreach (var item in spotRatings)
            {
                yield return new SpotRatingResponse(item);
            }
        }
    }
}
