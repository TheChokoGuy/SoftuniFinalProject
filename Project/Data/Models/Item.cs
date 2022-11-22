using System.ComponentModel.DataAnnotations;

namespace Project.Data.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 50)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int AvailableProducts { get; set; }

        //Add opportunity for reviews
    }
}
