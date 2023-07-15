using Api.Models.DTO.Request.Authorization.Registration;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityRegistrationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMailService _mailService;
        public IdentityRegistrationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {

            var user = new IdentityUser { Email = request.Email, UserName = request.Name };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                var message = new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Email confirmation",
                    Body = confirmationLink
                };
                await _mailService.SendEmailAsync(message);

                return Ok($"Welcome {request.Name}, your account successfully registerd!\nWe sent email verification code at {request.Email}");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
