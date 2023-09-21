using Api.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Api.Services.Helpers
{
    public static class UserService
    {
        public static string GetEmail(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null)
                return identity.FindFirst(ClaimTypes.Email)?.Value;

            return null;
        }
    }
}
