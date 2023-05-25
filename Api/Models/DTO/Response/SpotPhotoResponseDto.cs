using Api.Models.Entities;

namespace Api.Models.DTO.Response
{
    public class SpotPhotoResponseDto
    {
        public int Id { get; set; }
        public int SpotId { get; set; }
        public string Photo { get; set; }

        public SpotPhotoResponseDto(SpotPhoto spotPhoto)
        {
            Id = spotPhoto.Id;
            SpotId = spotPhoto.SpotId;
            Photo = spotPhoto.Photo is not null ? Convert.ToBase64String(spotPhoto.Photo) : string.Empty;
        }

        public static IEnumerable<SpotPhotoResponseDto> CreateResponse(List<SpotPhoto> spotPhotos)
        {
            foreach (var spotPhoto in spotPhotos)
            {
                yield return new SpotPhotoResponseDto(spotPhoto);
            }
        }
    }
}
