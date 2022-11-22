using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
