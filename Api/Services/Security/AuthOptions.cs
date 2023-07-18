using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Api.Services.Security
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public double AccessTokenExpirationHours { get; set; }
        public int RefreshTokensExpirationDays { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

        public TimeSpan GetAccessTokenExpirationTimeSpan() =>
            TimeSpan.FromHours(AccessTokenExpirationHours);

        public TimeSpan GetRefreshTokenExpirationTimeSpan() =>
            TimeSpan.FromDays(RefreshTokensExpirationDays);
    }
}
