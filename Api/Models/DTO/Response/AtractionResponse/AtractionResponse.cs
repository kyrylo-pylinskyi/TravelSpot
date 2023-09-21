namespace Api.Models.DTO.Response.AtractionResponse
{
    public class AtractionResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string ExternalId { get; set; }
        public int PlaceId { get; set; }
    }
}
