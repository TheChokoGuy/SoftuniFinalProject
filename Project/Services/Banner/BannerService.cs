using Project.Data.Models;
using Project.Data;
using System;

namespace Project.Services.Banner
{
    public class BannerService : IBannerService
    {
        private readonly ApplicationDbContext context;

        public BannerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddBannerAsync(Data.Models.Banner model)
        {
            await context.Banners.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public Task DeleteAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task EditBannerAsync(Item model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Data.Models.Banner>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetBannerAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetForEditAsync(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
