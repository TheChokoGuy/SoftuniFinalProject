using Project.Data.Models;
using Project.Models;
using System.Security.Claims;

namespace Project.Services
{
    public interface ICartService
    {
        public Task<string> AddToCartAsync(string cookie,int productId);

        public Task<IEnumerable<CartProductViewModel>> GetCartProducts(string cookie);

        public Task AddOrderAsync(Order order);
        public Task<IEnumerable<Order>> GetOrdersAsync();
    }
}
