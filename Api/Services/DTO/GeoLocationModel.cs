namespace Api.Services.DTO
{
    public class GeoLocationModel
    {
        public int PlaceId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
        public string Wikipedia { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
