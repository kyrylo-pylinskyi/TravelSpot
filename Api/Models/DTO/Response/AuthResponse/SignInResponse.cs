namespace Api.Models.DTO.Response.AuthResponse
{
    public class SignInResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
