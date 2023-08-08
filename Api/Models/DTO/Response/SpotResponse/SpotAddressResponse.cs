using Api.Models.Entities.Application;

namespace Api.Models.DTO.Response.SpotResponse
{
    public class SpotAddressResponse
    {
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public SpotAddressResponse(SpotAddress spotAddress)
        {
            Country = spotAddress.Country;
            State = spotAddress.State;
            City = spotAddress.City;
            Street = spotAddress.Street;
            Latitude = spotAddress.Latitude;
            Longitude = spotAddress.Longitude;
        }

        public static IEnumerable<SpotAddressResponse> CreateResponse(List<SpotAddress> spotAddresses)
        {
            foreach (var spotAddress in spotAddresses)
            {
                yield return new SpotAddressResponse(spotAddress);
            }
        }
    }
}
