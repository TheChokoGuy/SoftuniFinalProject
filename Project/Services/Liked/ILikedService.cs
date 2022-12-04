using Project.Models;

namespace Project.Services.Liked
{
    public interface ILikedService
    {
        public Task<string> AddToLikedAsync(string cookie, int productId);

        public Task<IEnumerable<ProductViewModel>> GetLikedAsync(string cookie);
    }
}
