using Api.Models.Entities;

namespace Api.Models.DTO.Response
{
    public class SpotResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SpotAddressResponseDto> Addresses { get; set; }
        public List<SpotPhotoResponseDto> Photos { get; set; }
    }
}
