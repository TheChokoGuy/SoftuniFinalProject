using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Project.Data.Models
{
    public class UserInformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(15)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(25)]
        public string City { get; set; }

        [Required]
        [StringLength(30)]
        public string Country { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(10)]
        public int PostalCode { get; set; }
    }
}
