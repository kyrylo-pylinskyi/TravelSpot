using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Request.Authorization.Login
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Field can't be empty")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
