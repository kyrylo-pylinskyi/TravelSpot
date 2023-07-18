using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response.Spot
{
    public class SpotCategoryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SpotId { get; set; }

        public SpotCategoryResponse(SpotCategory spotCategory)
        {
            Id = spotCategory.Id;
            Name = spotCategory.Name;
            Description = spotCategory.Description;
            SpotId = spotCategory.SpotId;
        }

        public static SpotCategoryResponse CreateResponse(SpotCategory spotCategory)
        {
            return new SpotCategoryResponse(spotCategory);
        }
    }
}
