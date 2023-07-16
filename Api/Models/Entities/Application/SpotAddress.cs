using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace Api.Models.Entities.Application
{
    public class SpotAddress
    {
        public int Id { get; set; }
        public string? PlaceId { get; set; }
        public string? OsmId { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int SpotId { get; set; }
        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
    }
}
