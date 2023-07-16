using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response
{
    public class SpotAddressResponseDto
    {
        public int Id { get; set; }
        public int SpotId { get; set; }
        public string? PlaceId { get; set; }
        public string? OsmId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public SpotAddressResponseDto(SpotAddress spotAddress)
        {
            Id = spotAddress.Id;
            SpotId = spotAddress.SpotId;
            PlaceId = spotAddress.PlaceId;
            OsmId = spotAddress.OsmId;
            Country = spotAddress.Country;
            State = spotAddress.State;
            City = spotAddress.City;
            Street = spotAddress.Street;
            Latitude = spotAddress.Latitude;
            Longitude = spotAddress.Longitude;
        }

        public static IEnumerable<SpotAddressResponseDto> CreateResponse(List<SpotAddress> spotAddresses)
        {
            foreach (var spotAddress in spotAddresses)
            {
                yield return new SpotAddressResponseDto(spotAddress);
            }
        }
    }
}
