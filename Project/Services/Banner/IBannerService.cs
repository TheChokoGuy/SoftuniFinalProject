namespace Project.Services.Banner
{
    using Project.Data.Models;
    using Project.Models;
    public interface IBannerService
    {
        Task AddBannerAsync(Banner model);

        Task DeleteAsync(int productId);

        Task<IEnumerable<Banner> > GetAllAsync();

        Task<Banner> GetBannerAsync(int productId);

        Task EditBannerAsync(Banner model);

    }
}
