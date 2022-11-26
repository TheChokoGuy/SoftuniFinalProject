using Project.Data.Models;
using System.Security.Claims;

namespace Project.Services
{
    public interface IUserService
    {
        public Task AddToCartAsync(int productId, string userId);

        public Task<IEnumerable<Item>> GetCartProducts(string userId);
    }
}
