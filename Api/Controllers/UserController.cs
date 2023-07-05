using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.DTO.Request.UserRequest;
using Api.Models.Entities;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;
        public UserController(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if(await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("This email address is used by another user");

            string validationCode = CreateVerificationCode();
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = HashCredentials(request.Password, out byte[] passwordSalt),
                PasswordSalt = passwordSalt,
                Role = UserRoles.Client,
                VerificationTokenHash = HashCredentials(validationCode, out byte[] verificationSalt),
                VerificationTokenSalt = verificationSalt,
                IsActive = false,
                IsEmailVerified = false,
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var mailRequest = new VerificationRequest()
            {
                ToEmail = user.Email,
                UserName = user.Name,
                Code = validationCode
            };

            await _mailService.SendVerificationEmailAsync(mailRequest);

            return Ok($"We sent email with verification code to {user.Email}");
        }

        [HttpPut]
        public async Task<IActionResult> NewEmailVerificationCode([FromForm] RegisterRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (VerifyCredentials(request.Password, user.PasswordHash, user.PasswordSalt))
                BadRequest("Bad credentials");

            string validationCode = CreateVerificationCode();
            user.VerificationTokenHash = HashCredentials(validationCode, out byte[] verificationSalt);
            user.VerificationTokenSalt = verificationSalt;
            _context.SaveChanges();

            var mailRequest = new VerificationRequest()
            {
                ToEmail = user.Email,
                UserName = user.Name,
                Code = validationCode
            };

            await _mailService.SendVerificationEmailAsync(mailRequest);

            return Ok($"We sent email with verification code to {user.Email}");
        }

        private byte[] HashCredentials(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return hash;
        }

        private bool VerifyCredentials(string password, byte[] hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }

        private string CreateVerificationCode() =>
            Convert.ToHexString(RandomNumberGenerator.GetBytes(3));
    }
}
