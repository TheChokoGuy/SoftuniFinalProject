using Microsoft.EntityFrameworkCore;
using Project.Data.Common;
using Project.Data;
using Project.Services.Banner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Services.Liked;
using Microsoft.Net.Http.Headers;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MockQueryable.Moq;
using Moq;
using Project.Data.Models;
using Project.Services;
using Project.Models;

namespace Project.UnitTests
{
    [TestFixture]
    public class LikedServiceTests
    {
        private IRepository repo;
        private ApplicationDbContext context;
        private ILikedService service;
        private IProductService pService;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("LikedDB")
                .Options;

            context = new ApplicationDbContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }

        [Test]
        public async Task TestAddToLikedAsync()
        {
            
            List<int> intCookieList = new List<int>()
            {
                1,
                2
            };

            repo = new Repository(context);
            service = new LikedService(repo);

            string cookie = JsonConvert.SerializeObject(intCookieList);

            string newCookie = await service.AddToLikedAsync(cookie, 1);

            List<int> cookieList = JsonConvert.DeserializeObject<List<int>>(newCookie);

            Assert.That(cookieList[0] == 1);

        }

        [Test]
        public async Task TestGetLikedAsync()
        {
            AddProductViewModel item1 = new AddProductViewModel()
            {
                Id = 1,
                AvailableProducts = 3,
                Description = "Blue",
                ImageUrl = "yes",
                Name = "Cube",
                Price = (decimal)74.99,
                CategoryId = 1,
            };

            AddProductViewModel item2 = new AddProductViewModel()
            {
                Id = 2,
                AvailableProducts = 5,
                Description = "Red",
                ImageUrl = "yes",
                Name = "Af",
                Price = (decimal)74.99,
                CategoryId = 2
            };

            repo = new Repository(context);
            service = new LikedService(repo);
            pService = new ProductService(repo);

            List<int> intCookieList = new List<int>()
            {
                1,
                2
            };

            string cookie = JsonConvert.SerializeObject(intCookieList);

            await pService.AddProductAsync(item1);
            await pService.AddProductAsync(item2);

            List<Item> products = service.GetLikedAsync(cookie).Result.Select(p => new Item(){
                Id= p.Id,
                AvailableProducts= p.AvailableProducts,
                Category = new Category()
                {
                    Id = p.CategoryId,
                    Name = p.Category
                },
                Description = p.Description,
                CategoryId= p.CategoryId,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                Name = p.Name
            }).ToList();

            Assert.That(products[0].Id == 1);
            Assert.That(products[1].Id == 2);

        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
