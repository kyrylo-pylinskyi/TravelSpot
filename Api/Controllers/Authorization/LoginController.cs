//using Api.Data;
//using Api.Models.DTO.Request.Authorization.Login;
//using Api.Services.Security;
//using Microsoft.AspNetCore.Mvc;

//namespace Api.Controllers.Authorization
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class LoginController : ControllerBase
//    {
//        private readonly AppDbContext _context;
//        public LoginController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Login([FromForm] LoginRequest request)
//        {
//            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
//            if (user == null)
//                return BadRequest("user not found");
//            if (!Credentials.VerifyHash(request.Password, user.PasswordHash, user.PasswordSalt))
//                return BadRequest("password is not correct");

//            var response = new LoginResponse()
//            {
//                Token = Credentials.CreateJwt(user),
//                Name = user.Name,
//                Email = request.Email,
//            };

//            return Ok(response);
//        }
//    }
//}
