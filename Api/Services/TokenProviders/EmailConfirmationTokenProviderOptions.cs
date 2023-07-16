using Microsoft.AspNetCore.Identity;

namespace Api.Services.TokenProviders
{
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            TokenLifespan = TimeSpan.FromDays(3); // Set the desired token lifespan
        }
    }
}
