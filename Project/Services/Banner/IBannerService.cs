namespace Project.Services.Banner
{
    using Project.Data.Models;
    using Project.Models;
    public interface IBannerService
    {
        Task AddBannerAsync(Banner model);

        Task<IEnumerable<Banner>> GetAllAsync();
        Task DeleteAsync(int productId);

        Task<Item> GetForEditAsync(int productId);

        Task EditBannerAsync(Item model);

        Task<Item> GetBannerAsync(int productId);
    }
}
