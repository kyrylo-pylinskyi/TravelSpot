using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response
{
    public class SpotCategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SpotId { get; set; }

        public SpotCategoryResponseDto(SpotCategory spotCategory)
        {
            Id = spotCategory.Id;
            Name = spotCategory.Name;
            Description = spotCategory.Description;
            SpotId = spotCategory.SpotId;
        }

        public static SpotCategoryResponseDto CreateResponse(SpotCategory spotCategory)
        {
            return new SpotCategoryResponseDto(spotCategory);
        }
    }
}
