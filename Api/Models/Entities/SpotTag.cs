using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities
{
    public class SpotTag
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
        public int SpotId { get; set; }
        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
    }
}
