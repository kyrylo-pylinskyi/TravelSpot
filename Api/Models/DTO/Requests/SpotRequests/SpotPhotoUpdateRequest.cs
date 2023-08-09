namespace Api.Models.DTO.Requests.SpotRequests
{
    public class SpotPhotoUpdateRequest
    {
        public int SpotPhotoId { get; set; }
        public IFormFile? SpotPhoto { get; set; }
    }
}
