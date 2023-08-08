using Api.Data;
using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotTagResponse
    {
        public string TagName { get; set; }
        public string TagDescription { get; set; }

        public SpotTagResponse(SpotTag spotTag)
        {
            TagName = spotTag.Tag.Name;
            TagDescription = spotTag.Tag.Description;
        }

        public static IEnumerable<SpotTagResponse> CreateResponse(List<SpotTag> spotTags)
        {
            foreach (var spotTag in spotTags)
            {
                yield return new SpotTagResponse(spotTag);
            }
        }
    }
}
