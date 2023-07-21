using Api.Models.DTO.Response.Auth;
using Api.Models.Entities.Identity;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services.Security
{
    public static class JwtService
    {
        public static SignInResponse GetUserAuthTokens(JwtAuthOptions authOptions, ApplicationUser user)
        {
            var accessTokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                });

            var refreshTokenClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                });

            return new SignInResponse
            {
                AccessToken = GetJwt(authOptions, accessTokenClaims, authOptions.GetAccessTokenExpirationTimeSpan()),
                RefreshToken = GetJwt(authOptions, refreshTokenClaims, authOptions.GetRefreshTokenExpirationTimeSpan())
            };
        }

        private static string GetJwt(JwtAuthOptions authOptions, ClaimsIdentity claims, TimeSpan exp)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(authOptions.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = authOptions.Audience,
                Issuer = authOptions.Issuer,
                Subject = claims,
                Expires = DateTime.UtcNow.Add(exp),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
