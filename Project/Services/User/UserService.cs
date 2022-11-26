using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Project.Data;
using Project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Project.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddToCartAsync(int productId, string userId)
        {
            var user = await context.Users
                            .FindAsync(userId);

            context.Users.Update(user);

            Item product = await context.Items.FindAsync(productId);

            user.Cart.Add(product);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Item>> GetCartProducts(string userId)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            List<Item> products = user.Cart.ToList();

            return products;
                                        
        }
    }
}
