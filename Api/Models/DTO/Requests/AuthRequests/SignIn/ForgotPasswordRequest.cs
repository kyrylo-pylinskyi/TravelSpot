using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Requests.AuthRequests.SignIn
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
    }
}
