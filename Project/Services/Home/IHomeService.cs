using Project.Data.Models;
using Project.Models;

namespace Project.Services.Home
{
    public interface IHomeService
    {
        Task<IEnumerable<Banner>> GetHomeProductsAsync();
    }
}
