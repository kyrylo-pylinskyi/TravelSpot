using Microsoft.AspNetCore.Identity;

namespace Api.Models.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Bio { get; set; }
        public List<UserPhoto> Photos { get; set; }
    }
}
