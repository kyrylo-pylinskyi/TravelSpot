using Api.Data;
using Api.Models.DTO.Request.Authorization.Login;
using Api.Services.Security;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleAuthorizationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public GoogleAuthorizationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = GoogleDefaults.AuthenticationScheme)]
        public void Login()
        {
            //TODO
        }
    }
}
