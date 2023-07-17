using Microsoft.AspNetCore.Identity;

namespace Api.Services.TokenProviders
{
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            TokenLifespan = TimeSpan.FromMinutes(15); // Set the desired token lifespan
        }
    }
}
