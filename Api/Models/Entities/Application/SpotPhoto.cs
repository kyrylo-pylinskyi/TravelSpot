using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Application
{
    public class SpotPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public int SpotId { get; set; }
        [ForeignKey(nameof(SpotId))]
        public Spot Spot { get; set; }
    }
}
