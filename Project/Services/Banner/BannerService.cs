using Project.Data.Models;
using Project.Data;
using System;
using Project.Data.Common;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteAsync(int productId)
        {
            await repo.DeleteAsync<Data.Models.Banner>(productId);
            await repo.SaveChangesAsync();
        }

        public async Task EditBannerAsync(Data.Models.Banner model)
        {
            var banner = await repo.GetByIdAsync<Data.Models.Banner>(model.Id);

            banner.Id = model.Id;
            banner.Title= model.Title;
            banner.ImageUrl= model.ImageUrl;

            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Data.Models.Banner>> GetAllAsync()
        {
            List<Data.Models.Banner> entities = await repo.AllReadonly<Data.Models.Banner>().ToListAsync();

            return entities;
        }

        public async Task<Data.Models.Banner> GetForEditAsync(int productId)
        {
            return await repo.GetByIdAsync<Data.Models.Banner>(productId);
        }
    }
}
