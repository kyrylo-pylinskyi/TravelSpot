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

        public GoogleController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                // Authentication successful, get the user's email from the claims
                var userEmail = result.Principal.FindFirstValue(ClaimTypes.Email);
                // Here, you can check if the user with this email already exists in your database.
                // If the user exists, you can sign them in.
                // If the user doesn't exist, you might consider creating a new user account.

                // Assuming you have a method in your UserManager to find or create the user based on the email.
                var user = await _userManager.FindByEmailAsync(userEmail);
                if (user == null)
                {
                    // User does not exist in the database. You might create a new user account here.
                    // Example:
                    user = new ApplicationUser
                    {
                        UserName = userEmail.Substring(0, userEmail.IndexOf('@')),
                        Email = userEmail,
                        EmailConfirmed = true,
                        // Set other user properties as needed.
                    };

                    // Create the new user in the database.
                    var createResult = await _userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        // Handle user creation failure if needed.
                        return BadRequest(new { msg = "User creation failed.", createResult });
                    }
                }
                //else
                //{
                //    // If the user already exists, update the EmailConfirmed property to true.
                //    user.EmailConfirmed = true;
                //    await _userManager.UpdateAsync(user);
                //}

                // Sign in the user using the SignInManager.
                await _signInManager.SignInAsync(user, isPersistent: false); // Change isPersistent as needed.

                return Ok("User logged in successfully.");
            }

            return Unauthorized("Google authentication failed.");
        }
    }

}
