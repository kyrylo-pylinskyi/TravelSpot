namespace Api.Models.DTO.Request.Authorization.Login
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
