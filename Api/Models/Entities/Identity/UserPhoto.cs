using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models.Entities.Identity
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public bool IsMainPhoto { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
