using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Services.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "TravelSpotApi"; 
        public const string AUDIENCE = "TravelSpotClient"; 
        const string KEY = "travelspot_supersecret_secretkey!123";  
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
