using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
