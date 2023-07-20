using Api.Models.DTO.Request.Auth.SignIn;
using Api.Models.DTO.Response.Auth;
using Api.Models.Entities.Identity;
using Api.Services.Security;
using Api.Services.Smtp;
using Api.Services.Smtp.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers.AuthControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtAuthOptions _authOptions;
        private readonly IMailService _mailService;

        public SignInController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JwtAuthOptions> authOptions,
            IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authOptions = authOptions.Value;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] SignInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail) ??
               await _userManager.FindByNameAsync(request.UserNameOrEmail);


            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized();
            }

            var tokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                });

            var refreshTokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                });

            var accessToken = GetJwt(tokenClaims, _authOptions.GetAccessTokenExpirationTimeSpan());
            var refreshToken = GetJwt(refreshTokenClaims, _authOptions.GetRefreshTokenExpirationTimeSpan());

            return Ok(new SignInResponse { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string requestToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Read and validate the token without signature validation
            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(requestToken);

            // Extract the required values from jwt
            string nameid = jwtToken.Claims.First(c => c.Type == "nameid").Value;
            string exp = jwtToken.Claims.First(c => c.Type == "exp").Value;
            var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).DateTime;

            var user = await _userManager.FindByIdAsync(nameid);

            if (user == null)
                return BadRequest(new AuthResponse { Message = "InvalidToken", UserEmail = "" });

            if (DateTime.UtcNow > expirationDateTime)
                return Unauthorized(new AuthResponse { Message = "InvalidToken", UserEmail = user.Email });

            var tokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                });

            var refreshTokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                });

            var accessToken = GetJwt(tokenClaims, _authOptions.GetAccessTokenExpirationTimeSpan());
            var refreshToken = GetJwt(refreshTokenClaims, _authOptions.GetRefreshTokenExpirationTimeSpan());

            return Ok(new SignInResponse{ AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            // Generate an email verification token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Build the verification URL
            var callbackUrl = Url.Action("ResetPassword", "Login", new { userId = user.Id, token }, Request.Scheme);

            var message = new MailRequest
            {
                ToEmail = user.Email,
                Subject = "Confirm your email",
                Body = $"Please confirm your account by clicking this <a href='{callbackUrl}'>link</a>."
            };

            await _mailService.SendEmailAsync(message);

            return Ok(new AuthResponse { Message = "OK", UserEmail = user.Email });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

            if (result.Succeeded)
            {
                var message = new MailRequest
                {
                    ToEmail = user.Email,
                    Subject = "Password changed",
                    Body = $"Password changed!"
                };

                await _mailService.SendEmailAsync(message);

                return Ok(new AuthResponse { Message = "OK", UserEmail = user.Email });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private string GetJwt(ClaimsIdentity claimsIdentity, TimeSpan expirence)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_authOptions.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _authOptions.Audience,
                Issuer = _authOptions.Issuer,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.Add(expirence),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
