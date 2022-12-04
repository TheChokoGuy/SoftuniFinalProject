using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Project.Data;
using Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Models;

namespace Project.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CartService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task<string> AddToCartAsync(string cookie, int productId)
        {
            List<int> listCart = JsonConvert.DeserializeObject<List<int>>(cookie);

            if(!listCart.Contains(productId))
            {
                listCart.Add(productId);
            }

            string jsonCart = JsonConvert.SerializeObject(listCart);

            return jsonCart;

        }

        public async Task<IEnumerable<ProductViewModel>> GetCartProducts(string cookie)
        {

            List<int> listCart = JsonConvert.DeserializeObject<List<int>>(cookie);

            List<Item> items = new List<Item>();

            foreach (var item in listCart)
            {
                items.Add(await context.Items.FindAsync(item));
            }

            List<ProductViewModel> models = new List<ProductViewModel>();


            foreach (var item in items)
            {
                Category cateogry = await context.Categories.FindAsync(item.CategoryId);

                models.Add(new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    AvailableProducts = item.AvailableProducts,
                    Category = cateogry.Name,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price
                }); 
            }

            return models;
        }
    }
}
