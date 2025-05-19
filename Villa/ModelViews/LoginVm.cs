using System.ComponentModel.DataAnnotations;

namespace Villa.ModelViews
{
    public class LoginVm
    {
        [Required(ErrorMessage = "UserName or Email is required")]
        public string UserNameOrEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
