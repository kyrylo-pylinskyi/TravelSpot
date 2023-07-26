using Microsoft.AspNetCore.Http.Metadata;

namespace Api.Models.DTO.Requests.SpotRequests
{
    public class SpotPhotoRequest
    {
        public int SpotId { get; set; }
        public IFormFile? SpotPhoto { get; set; }
    }
}
