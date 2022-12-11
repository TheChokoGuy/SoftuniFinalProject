namespace Project.Models
{
    public class CartProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Category { get; set; }
        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }


    }
}
