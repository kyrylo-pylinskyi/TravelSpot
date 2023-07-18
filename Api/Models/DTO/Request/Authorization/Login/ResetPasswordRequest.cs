using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Request.Authorization.Login
{
    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Field can't be empty")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string Token { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}
