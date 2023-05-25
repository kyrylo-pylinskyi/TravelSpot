using Microsoft.AspNetCore.Http.Metadata;

namespace Api.Models.DTO.Request
{
    public class SpotPhotoRequestDto
    {
        public int SpotId { get; set; }
        public IFormFile? SpotPhoto { get; set;}
    }
}
