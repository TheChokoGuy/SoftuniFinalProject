namespace Project.Services.Home
{
    using Microsoft.EntityFrameworkCore;
    using Project.Data;
    using Project.Data.Common;
    using Project.Data.Models;
    using Project.Models;

    public class HomeService : IHomeService
    {
        private readonly IRepository repo;

        public HomeService(IRepository _repo)
        {
            this.repo = _repo;
        }


        public async Task<IEnumerable<Banner>> GetHomeProductsAsync()
        {
            List<Banner> result = await repo.AllReadonly<Banner>()
                .OrderByDescending(h => h.Id)
                .Take(5)
                .ToListAsync();

            return result;
        }
    }
}
