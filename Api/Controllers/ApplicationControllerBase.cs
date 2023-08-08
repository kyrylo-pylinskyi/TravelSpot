using Api.Data;
using Api.Models.Entities.Identity;
using Api.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ApplicationControllerBase : ControllerBase
    {
        protected readonly UserManager<ApplicationUser> _userManager;
        protected readonly AppDbContext _context;

        public ApplicationControllerBase(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        protected async Task<ApplicationUser> GetAuthorizedUser()
        {
            string email = UserService.GetEmail(HttpContext);
            if (string.IsNullOrEmpty(email))
                return null;

            var user = await _userManager.FindByEmailAsync(email);

            return user;
        }
    }
}
