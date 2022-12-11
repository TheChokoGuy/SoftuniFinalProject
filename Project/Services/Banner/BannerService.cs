using Project.Data.Models;
using Project.Data;
using System;
using Project.Data.Common;

namespace Project.Services.Banner
{
    public class BannerService : IBannerService
    {
        private readonly IRepository repo;

        public BannerService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task AddBannerAsync(Data.Models.Banner model)
        {
            await repo.AddAsync<Data.Models.Banner>(model);
            await repo.SaveChangesAsync();
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
