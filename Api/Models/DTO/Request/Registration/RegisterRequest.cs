using System.ComponentModel.DataAnnotations;

namespace Api.Models.DTO.Request.Registration
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Field can't be empty")]
        [StringLength(25, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 3)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Field can't be empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

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
