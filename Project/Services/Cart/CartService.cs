using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Project.Data;
using Project.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Models;
using Project.Data.Common;

namespace Project.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository repo;

        public CartService(IRepository repo)
        {
            this.repo = repo;

        }

        public async Task AddOrderAsync(Order order)
        {
            await repo.AddAsync<Order>(order);

            await repo.SaveChangesAsync();
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

        public async Task<IEnumerable<CartProductViewModel>> GetCartProducts(string cookie)
        {
            if (cookie == null)
                return new List<CartProductViewModel>();

            List<int> listCart = JsonConvert.DeserializeObject<List<int>>(cookie);

            List<Item> items = new List<Item>();

            foreach (var item in listCart)
            {
                items.Add(await repo.GetByIdAsync<Item>(item));
            }

            List<CartProductViewModel> models = new List<CartProductViewModel>();


            foreach (var item in items)
            {
                Category cateogry = await repo.GetByIdAsync<Category>(item.CategoryId);

                models.Add(new CartProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price,
                    Category = cateogry.Name,
                    CategoryId = item.CategoryId,
                    Description = item.Description
                }); 
            }

            return models;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string userId)
        {
            List<Order> orders = await repo.AllReadonly<Order>().Where(o => o.UserId == userId).ToListAsync();
            return orders;
        }
    }
}
