using Api.Models.Entities;

namespace Api.Models.DTO.Response
{
    public class SpotResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double RatingAvg { get { return Ratings.Select(r => r.Rating).Average(); } }
        public string CategoryName { get { return Category.Name; } }
        public string CategoryDescription { get { return Category.Description; } }
        public SpotCategoryResponseDto Category { get; set; }
        public List<SpotTagResponseDto> Tags { get; set; }
        public List<SpotAddressResponseDto> Addresses { get; set; }
        public List<SpotRatingResponseDto> Ratings { get; set; }
        public List<SpotPhotoResponseDto> Photos { get; set; }
    }
}
