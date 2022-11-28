namespace Project.Services.Home
{
    using Project.Data;
    using Project.Data.Models;
    using Project.Models;

    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext context;

        public HomeService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Banner>> GetHomeProductsAsync()
        {
            return this.context.Banners.OrderByDescending(i => i.Id).Take(5);
        }
    }
}
