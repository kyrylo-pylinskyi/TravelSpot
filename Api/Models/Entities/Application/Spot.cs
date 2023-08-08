using Api.Models.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Application
{
    public class Spot
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public SpotCategory? Category { get; set; }
        public List<SpotRate>? Rates { get; set; }
        public List<SpotTag>? Tags { get; set; }
        public List<SpotPhoto>? Photos { get; set; }
        public List<SpotAddress>? Addresses { get; set; }
    }
}
