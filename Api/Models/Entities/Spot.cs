namespace Api.Models.Entities
{
    public class Spot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SpotCategory? Category { get; }
        public List<SpotRating>? Ratings { get; }
        public List<SpotTag>? Tags { get; }
        public List<SpotPhoto>? Photos { get; }
        public List<SpotAddress>? Addresses { get; }
    }
}
