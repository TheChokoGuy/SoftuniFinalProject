using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }

        [Required]
        public string ImageUrl { get; set; }

    }
}
