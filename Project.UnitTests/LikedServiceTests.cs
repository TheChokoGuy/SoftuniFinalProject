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

namespace Project.UnitTests
{
    [TestFixture]
    public class LikedServiceTests
    {
        private IRepository repo;
        private ApplicationDbContext context;
        private ILikedService service;
        private HttpRequest request;

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
            request.Headers.Add("LikedTest", new CookieHeaderValue("LikedTest", JsonConvert.SerializeObject(new List<int>())).ToString());

            string cookie = request.Cookies["LikedTest"];

            var repoMock = new Mock<IRepository>();

            List<Item> items = new List<Item>()
            {
                new Item()
                {
                    Id = 1,
                    AvailableProducts = 3,
                    Description = "Blue",
                    ImageUrl = "yes",
                    Name = "Cube",
                    Price = (decimal)74.99,
                    CategoryId = 1,
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "Dress"
                    }
                },
                new Item()
                {
                    Id = 2,
                    AvailableProducts = 5,
                    Description = "Red",
                    ImageUrl = "yes",
                    Name = "Af",
                    Price = (decimal)74.99,
                    Category = new Category()
                    {
                        Id = 2,
                        Name = "Skirt"
                    },
                    CategoryId = 2
                }

            };

            repoMock.Setup(r => r.AllReadonly<Item>())
                .Returns(items.AsQueryable().BuildMock());

            var repo = repoMock.Object;
            service = new LikedService(repo);

            string newCookie = await service.AddToLikedAsync(cookie, 1);

            request.Headers.Add("LikedTest", new CookieHeaderValue("LikedTest", newCookie).ToString());

            List<int> cookieList = JsonConvert.DeserializeObject<List<int>>(newCookie);

            Assert.That(cookieList[0] == 1);

        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
