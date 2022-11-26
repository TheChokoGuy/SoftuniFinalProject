using Project.Data;
using Project.Data.Models;
using Project.Models;

namespace Project.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext context;

        public HomeService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Banner>> GetHomeProductsAsync()
        {
            return this.context.Items.OrderByDescending(i => i.Id).Select(i => new Banner
            {
                Id = i.Id,
                Title = i.Name,
                ImageUrl = i.ImageUrl
            }).Take(5);
        }
    }
}
