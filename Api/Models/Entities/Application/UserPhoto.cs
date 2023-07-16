using System.ComponentModel.DataAnnotations.Schema;
using Api.Models.Entities.Identity;

namespace Api.Models.Entities.Application
{
    public class UserPhoto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
