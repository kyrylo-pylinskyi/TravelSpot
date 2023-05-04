using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities
{
    public class SpotTag
    {
        public int Id { get; set; }
        public Tag Tag { get; set; }
        public int SpotId { get; set; }
        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
    }

    public enum Tag
    {
        None = 0,
        Nature = 1,
        Culture = 2,
        Adventure = 3,
        Food = 4,
        Relaxation = 5,
        History = 6,
        Sports = 7,
        Nightlife = 8,
        Shopping = 9,
        Beaches = 10,
        Mountains = 11,
        Wildlife = 12,
        FamilyFriendly = 13,
        Rural = 14,
        Waterfalls = 15
    }
}
