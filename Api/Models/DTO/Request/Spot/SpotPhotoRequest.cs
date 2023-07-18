using Microsoft.AspNetCore.Http.Metadata;

namespace Api.Models.DTO.Request.Spot
{
    public class SpotPhotoRequest
    {
        public int SpotId { get; set; }
        public IFormFile? SpotPhoto { get; set; }
    }
}
