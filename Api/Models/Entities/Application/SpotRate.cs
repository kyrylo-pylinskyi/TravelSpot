using Api.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Application
{
    public class SpotRate
    {
        public int Id { get; set; }
        public Rate Rate { get; set; }
        public int SpotId { get; set; }
        public string RaterId { get; set; }
        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
        [ForeignKey(nameof(RaterId))]
        public ApplicationUser Rater { get; set; }
    }
    public enum Rate : int
    {
        OneStar = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5
    }
}
