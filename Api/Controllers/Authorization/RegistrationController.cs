using Api.Models.DTO.Request.Authorization.Registration;
using Api.Models.Entities.Identity;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        public RegistrationController(UserManager<ApplicationUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {

            var user = new ApplicationUser { UserName = request.Name, Email = request.Email};
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Generate an email verification token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // Build the verification URL
                var callbackUrl = Url.Action("ConfirmEmail", "Registration", new { userId = user.Id, token }, Request.Scheme);

                var message = new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Confirm your email",
                    Body = $"Please confirm your account by clicking this <a href='{callbackUrl}'>link</a>"
                };

                await _mailService.SendEmailAsync(message);

                return Ok(new {msg = "OK", user.Id, token, callbackUrl } );
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        [Route("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("User Id and token can not be null");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok(new { msg = "OK", user.Id, user.Email });
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut]
        [Route("ResendConfirmation")]
        public async Task<IActionResult> ResendConfirmation([FromForm] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (user.EmailConfirmed)
            {
                return BadRequest("Email already confirmed");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Registration", new { userId = user.Id, token }, Request.Scheme);

            var message = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = $"Please confirm your account by clicking this <a href='{callbackUrl}'>link</a>."
            };

            await _mailService.SendEmailAsync(message);

            return Ok(new { msg = "OK", user.Id, token, callbackUrl });
        }

    }
}
