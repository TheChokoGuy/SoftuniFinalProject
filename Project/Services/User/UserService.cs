using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Project.Data;
using Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Models;

namespace Project.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;

        }
        public async Task AddToCartAsync(int productId)
        {
            var cart = new List<int>();

            var jsonCart = JsonConvert.SerializeObject(cart);

            if (_session.Get("Cart") == null)
            {
                _session.SetString("Cart", jsonCart);
            }

            var sessionCart = _session.GetString("Cart");

            List<int> listCart = JsonConvert.DeserializeObject<List<int>>(sessionCart);

            if(!listCart.Contains(productId))
            {
                listCart.Add(productId);
            }

            jsonCart = JsonConvert.SerializeObject(listCart);
            _session.SetString("Cart", jsonCart);

        }

        public async Task<IEnumerable<ProductViewModel>> GetCartProducts()
        {
            var cart = new List<int>();

            var jsonCart = JsonConvert.SerializeObject(cart);

            if (_session.Get("Cart") == null)
            {
                _session.SetString("Cart", jsonCart);
            }

            List<int> listCart = JsonConvert.DeserializeObject<List<int>>(_session.GetString("Cart"));

            List<Item> items = new List<Item>();

            foreach (var item in listCart)
            {
                items.Add(await context.Items.FindAsync(item));
            }

            List<ProductViewModel> models = new List<ProductViewModel>();

            foreach (var item in items)
            {
                models.Add(new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    AvailableProducts = item.AvailableProducts,
                    Category = context.Categories.FindAsync(item.CategoryId).ToString(),
                    ImageUrl = item.ImageUrl,
                    Price = item.Price
                });
            }

            return models;
        }
    }
}
