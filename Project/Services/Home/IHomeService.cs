namespace Project.Services.Home
{
    using Project.Data.Models;
    using Project.Models;
    public interface IHomeService
    {
        Task<IEnumerable<Banner>> GetHomeProductsAsync();
    }
}
