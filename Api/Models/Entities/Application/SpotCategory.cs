using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Application
{
    public class SpotCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int SpotId { get; set; }

        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
    }
}
