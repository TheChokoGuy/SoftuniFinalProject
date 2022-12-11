using Newtonsoft.Json;
using Project.Data;
using Project.Data.Common;
using Project.Data.Models;
using Project.Models;

namespace Project.Services.Liked
{
    public class LikedService : ILikedService
    {
        private readonly IRepository repo;
        public LikedService(IRepository repo)
        {
            this.repo = repo;
        }

        public async Task<string> AddToLikedAsync(string cookie, int productId)
        {
            List<int> listLiked = JsonConvert.DeserializeObject<List<int>>(cookie);

            if (!listLiked.Contains(productId))
            {
                listLiked.Add(productId);
            }

            string jsonLiked = JsonConvert.SerializeObject(listLiked);

            return jsonLiked;
        }

        public async Task<IEnumerable<ProductViewModel>> GetLikedAsync(string cookie)
        {
            if(cookie == null)
                return new List<ProductViewModel>();

            List<int> listLiked = JsonConvert.DeserializeObject<List<int>>(cookie);
            
            List<Item> items = new List<Item>();

            foreach (var item in listLiked)
            {
                items.Add(await repo.GetByIdAsync<Item>(item));
            }

            List<ProductViewModel> models = new List<ProductViewModel>();


            foreach (var item in items)
            {
                Category cateogry = await repo.GetByIdAsync<Category>(item.CategoryId);

                models.Add(new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    AvailableProducts = item.AvailableProducts,
                    CategoryId = item.CategoryId,
                    Category = cateogry.Name,
                    ImageUrl = item.ImageUrl,
                    Price = item.Price
                });
            }

            return models;
        }
    }
}
