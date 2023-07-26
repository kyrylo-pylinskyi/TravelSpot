using Api.Models.Entities;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double RatingAvg { get { return Ratings.Select(r => r.Rating).Average(); } }
        public string CategoryName { get { return Category.Name; } }
        public string CategoryDescription { get { return Category.Description; } }
        public SpotCategoryResponse Category { get; set; }
        public List<SpotTagResponse> Tags { get; set; }
        public List<SpotAddressResponse> Addresses { get; set; }
        public List<SpotRatingResponse> Ratings { get; set; }
        public List<SpotPhotoResponse> Photos { get; set; }
    }
}
