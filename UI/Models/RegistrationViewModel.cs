using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class RegistrationViewModel
    {

        public int Id { get; set; }


        [StringLength(100)]
        public string UserName { get; set; } = null!;

        [StringLength(100)]
        public string Email { get; set; } = null!;

        [StringLength(15)]
        public string Mobile { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;
    }
}
