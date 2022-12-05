using Project.Data.Models;
using Project.Models;

namespace Project.Services
{
    public interface IProductService
    {
        Task AddProductAsync(AddProductViewModel model);
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<IEnumerable<ProductViewModel>> GetAllAsync();
        Task DeleteAsync(int productId);

        Task<Item> GetForEditAsync(int productId);

        Task EditProductAsync(Item model);
        Task<List<Item>> GetProductByStringAsync(string text);

        Task<Item> GetProductAsync(int productId);
    }
}
