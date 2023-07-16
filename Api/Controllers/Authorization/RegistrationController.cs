using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.DTO.Request.Authorization.Registration;
using Api.Models.Entities.Application;
using Api.Services.Security;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Api.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;
        public RegistrationController(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("This email address is used by another user");

            string validationCode = Credentials.CreateVerificationCode();
            var user = new User()
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = Credentials.CreateHash(request.Password, out byte[] passwordSalt),
                PasswordSalt = passwordSalt,
                Role = UserRoles.Client,
                VerificationTokenHash = Credentials.CreateHash(validationCode, out byte[] verificationSalt),
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
        [Route("resend-code")]
        public async Task<IActionResult> ResendVerificationCode([FromForm] RegisterRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (!Credentials.VerifyHash(request.Password, user.PasswordHash, user.PasswordSalt))
                BadRequest("Bad credentials");

            string validationCode = Credentials.CreateVerificationCode();
            user.VerificationTokenHash = Credentials.CreateHash(validationCode, out byte[] verificationSalt);
            user.VerificationTokenSalt = verificationSalt;
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
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromForm] ConfirmEmailRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return BadRequest("User not found");

            if (!Credentials.VerifyHash(request.Code, user.VerificationTokenHash, user.VerificationTokenSalt))
                return BadRequest("Invalid code");

            user.VerificationTokenHash = null;
            user.VerificationTokenSalt = null;
            user.IsEmailVerified = true;
            user.IsActive = true;

            await _context.SaveChangesAsync();

            return Ok("Email address verified");
        }
    }
}
