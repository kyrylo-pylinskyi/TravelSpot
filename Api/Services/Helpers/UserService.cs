using System.Security.Claims;

namespace Api.Services.Helpers
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetEmail()
        {
            if (_httpContextAccessor.HttpContext is null)
                return "HTTP Context Accessor is null";

            var userContext = _httpContextAccessor.HttpContext.User;
            return userContext.FindFirstValue(ClaimTypes.Email);
        }
    }
}
