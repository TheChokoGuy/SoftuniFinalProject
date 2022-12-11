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
        public async Task<string> AddToCartAsync(string cookie, int productId)
        {
            Dictionary<int, int> listCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie);

            if(!listCart.ContainsKey(productId))
            {
                listCart.Add(productId, 1);
            }

            string jsonCart = JsonConvert.SerializeObject(listCart);

            return jsonCart;

        }

        public async Task<IEnumerable<CartProductViewModel>> GetCartProducts(string cookie)
        {
            if (cookie == null)
                return new List<CartProductViewModel>();

            Dictionary<int, int> listCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookie);

            List<Item> items = new List<Item>();

            foreach (var item in listCart)
            {
                items.Add(await repo.GetByIdAsync<Item>(item.Key));
            }

            List<CartProductViewModel> models = new List<CartProductViewModel>();


            foreach (var item in items)
            {
                models.Add(new CartProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price,
                    Quantity = listCart.FirstOrDefault(c => c.Key == item.Id).Value
                }); 
            }

            return models;
        }
    }
}
