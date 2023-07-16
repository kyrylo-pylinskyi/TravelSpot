using Api.Data;
using Api.Models.DTO.Request.Authorization.ResetPassword;
using Api.Services.Security;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authorization
{
    public class ResetPasswordController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMailService _mailService;
        public ResetPasswordController(AppDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("reset-password-request")]
        public async Task<IActionResult> RequestPasswordReset([FromForm] ResetPasswordRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return NotFound("User not found");

            string validationCode = Credentials.CreateVerificationCode();
            user.VerificationTokenHash = Credentials.CreateHash(validationCode, out byte[] salt);
            user.VerificationTokenSalt = salt;
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

        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] SetPasswordRequest request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return NotFound("User not found");

            if (!Credentials.VerifyHash(request.Code, user.VerificationTokenHash, user.VerificationTokenSalt))
                return BadRequest("Invalid verification code");

            user.VerificationTokenHash = null;
            user.VerificationTokenSalt = null;

            user.PasswordHash = Credentials.CreateHash(request.Password, out byte[] salt);
            user.PasswordSalt = salt;

            await _context.SaveChangesAsync();

            var mailRequest = new VerificationRequest()
            {
                ToEmail = user.Email,
                UserName = user.Name,
                Code = "Password changed"
            };

            await _mailService.SendVerificationEmailAsync(mailRequest);

            return Ok($"Password changed : {user.Email}");
        }
    }
}
