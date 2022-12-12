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
    public class CartServiceTests
    {
        private IRepository repo;
        private ApplicationDbContext context;
        private ICartService service;
        private IProductService pService;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("CartDB")
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
            service = new CartService(repo);

            string cookie = JsonConvert.SerializeObject(intCookieList);

            string newCookie = await service.AddToCartAsync(cookie, 1);

            List<int> cookieList = JsonConvert.DeserializeObject<List<int>>(newCookie);

            Assert.That(cookieList[0] == 1);

        }

        [Test]
        public async Task TestGetCartAsync()
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
            service = new CartService(repo);
            pService = new ProductService(repo);

            List<int> intCookieList = new List<int>()
            {
                1,
                2
            };

            string cookie = JsonConvert.SerializeObject(intCookieList);

            await pService.AddProductAsync(item1);
            await pService.AddProductAsync(item2);

            List<Item> products = service.GetCartProducts(cookie).Result.Select(p => new Item(){
                Id = p.Id,
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

        [Test]
        public async Task TestGetOrdersAsync()
        {
            var repoMock = new Mock<IRepository>();
            List<Order> orders = new List<Order>()
            {
                new Order() { Id = 1, FirstName="Gabriel", LastName="Goranov", Address="", Card="", City="", Country="", Date="", PhoneNumber="", PostalCode="", Price=33.99M, UserId ="" },
                new Order() { Id = 2, FirstName="GOSHO", LastName="petroc", Address="", Card="", City="", Country="", Date="", PhoneNumber="", PostalCode="", Price=33.99M, UserId ="" },
            };

            repoMock.Setup(r => r.AllReadonly<Order>())
                .Returns(orders.AsQueryable().BuildMock());

            repo = repoMock.Object;

            service = new CartService(repo);
            IEnumerable<Order> products = await service.GetOrdersAsync();

            Assert.That(products.Any(o => o.FirstName == orders[0].FirstName) == true);
            Assert.That(products.Any(o => o.FirstName == orders[1].FirstName) == true);
        }

        [Test]
        public async Task TestAddOrderAsync()
        {
            List<Order> orders = new List<Order>()
            {
                new Order() { Id = 1, FirstName="Gabriel", LastName="Goranov", Address="", Card="", City="", Country="", Date="", PhoneNumber="", PostalCode="", Price=33.99M, UserId ="" },
                new Order() { Id = 2, FirstName="GOSHO", LastName="petroc", Address="", Card="", City="", Country="", Date="", PhoneNumber="", PostalCode="", Price=33.99M, UserId ="" },
            };

            repo = new Repository(context);
            service = new CartService(repo);

            await service.AddOrderAsync(orders[0]);
            await service.AddOrderAsync(orders[1]);

            var ords = await service.GetOrdersAsync();

            Assert.That(ords.Any(o => o.FirstName == orders[0].FirstName) == true);
            Assert.That(ords.Any(o => o.FirstName == orders[1].FirstName) == true);
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
