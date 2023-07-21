using Api.Data;
using Api.Services.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Api.Models.Entities.Identity;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Api.Controllers.AuthControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtAuthOptions _authOptions;

        public GoogleController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtAuthOptions authOptions)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("SignIn")]
        public async Task SignIn()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (result.Succeeded)
            {
                var userEmail = result.Principal.FindFirstValue(ClaimTypes.Email);

                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    // Create the new user in the database.
                    user = new ApplicationUser
                    {
                        UserName = userEmail.Substring(0, userEmail.IndexOf('@')),
                        Email = userEmail,
                        EmailConfirmed = true,
                    };

                    var createResult = await _userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        return BadRequest(new { msg = "User creation failed.", createResult });
                    }
                }

                // Sign in the user using the SignInManager.
                await _signInManager.SignInAsync(user, isPersistent: false);

                return Ok(JwtService.GetUserAuthTokens(_authOptions, user));
            }

            return Unauthorized("Google authentication failed.");
        }
    }

}
