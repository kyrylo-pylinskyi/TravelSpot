using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotPhotoResponse
    {
        public int Id { get; set; }
        public int SpotId { get; set; }
        public string Photo { get; set; }

        public SpotPhotoResponse(SpotPhoto spotPhoto)
        {
            Id = spotPhoto.Id;
            SpotId = spotPhoto.SpotId;
            Photo = spotPhoto.Photo is not null ? Convert.ToBase64String(spotPhoto.Photo) : string.Empty;
        }

        public static IEnumerable<SpotPhotoResponse> CreateResponse(List<SpotPhoto> spotPhotos)
        {
            foreach (var spotPhoto in spotPhotos)
            {
                yield return new SpotPhotoResponse(spotPhoto);
            }
        }
    }
}
