using Microsoft.AspNetCore.Identity;

namespace Api.Models.Entities.Application
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRoles Role { get; set; }
        public byte[]? Photo { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[]? VerificationTokenHash { get; set; }
        public byte[]? VerificationTokenSalt { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEmailVerified { get; set; }
    }
}
