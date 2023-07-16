using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Application
{
    public class SpotRating
    {
        public int Id { get; set; }
        public Rating Rating { get; set; }
        public int SpotId { get; set; }
        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
    }
    public enum Rating
    {
        None = 0,
        OneStar = 1,
        TwoStars = 2,
        ThreeStars = 3,
        FourStars = 4,
        FiveStars = 5
    }
}
