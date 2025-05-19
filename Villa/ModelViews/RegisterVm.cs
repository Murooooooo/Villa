using System.ComponentModel.DataAnnotations;

namespace Villa.ModelViews
{
    public class RegisterVm
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(50, ErrorMessage = "UserName cannot exceed 50 characters")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
