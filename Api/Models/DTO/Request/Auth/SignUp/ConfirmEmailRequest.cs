using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Request.Auth.SignUp
{
    public class ConfirmEmailRequest
    {
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(6, ErrorMessage = "Must be between 6 characters", MinimumLength = 6)]
        public string Code { get; set; }
    }
}
