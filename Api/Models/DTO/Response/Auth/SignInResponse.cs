namespace Api.Models.DTO.Response.Auth
{
    public class SignInResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
