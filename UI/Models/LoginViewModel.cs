using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage ="Password is required")]
        [StringLength(255)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage ="Confirmpassword")]
        [StringLength(255)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
