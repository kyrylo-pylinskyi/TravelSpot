//using Microsoft.AspNetCore.Authentication.OAuth;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace Api.Services.Security
//{
//    public class JwtService
//    {
//        private string GetJwt(ClaimsIdentity claimsIdentity, TimeSpan expirence)
//        {
//            var tokenHandler = new JwtSecurityTokenHandler();
//            var key = Encoding.UTF8.GetBytes(_authOptions.Key);

//            var tokenDescriptor = new SecurityTokenDescriptor
//            {
//                Audience = _authOptions.Audience,
//                Issuer = _authOptions.Issuer,
//                Subject = claimsIdentity,
//                Expires = DateTime.UtcNow.Add(expirence),
//                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
//            };

//            var token = tokenHandler.CreateToken(tokenDescriptor);
//            var tokenString = tokenHandler.WriteToken(token);

//            return tokenString;
//        }
//    }
//}
