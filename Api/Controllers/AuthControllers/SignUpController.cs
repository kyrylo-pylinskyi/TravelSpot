using Api.Models.DTO.Requests.AuthRequests.SignUp;
using Api.Models.DTO.Response.AuthResponse;
using Api.Models.Entities.Identity;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AuthControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignUpController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        public SignUpController(UserManager<ApplicationUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] SignUpRequest request)
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

                return Ok(new AuthResponse{ Message = "OK", UserEmail = user.Email } );
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
                return Ok(new AuthResponse { Message = "OK", UserEmail = user.Email });
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
                return BadRequest(new AuthResponse { Message = "UserNotFound", UserEmail = email });
            }

            if (user.EmailConfirmed)
            {
                return BadRequest(new AuthResponse { Message = "EmailAlreadyConfirmed", UserEmail = email });
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

            return Ok(new AuthResponse { Message = "OK", UserEmail = user.Email });
        }

    }
}
