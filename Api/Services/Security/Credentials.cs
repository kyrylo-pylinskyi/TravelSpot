using Api.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Api.Services.Security
{
    public static class Credentials
    {
        private const int keySize = 64;
        private const int iterations = 350000;

        public static byte[] CreateHash(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
            iterations,
                HashAlgorithmName.SHA512,
                keySize);

            return hash;
        }

        public static bool VerifyHash(string password, byte[] hash, byte[] salt)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA512, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
        }

        public static string CreateVerificationCode() =>
            Convert.ToHexString(RandomNumberGenerator.GetBytes(3));

        public static string CreateJwt(User user)
        {
            var claims = new List<Claim> { 
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
