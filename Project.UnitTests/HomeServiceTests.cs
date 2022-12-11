using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MockQueryable.Moq;
using Moq;
using Project.Data;
using Project.Data.Common;
using Project.Data.Models;
using Project.Services.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Project.UnitTests
{
    [TestFixture]
    public class HomeServiceTests
    {
        private IRepository repo;
        private ApplicationDbContext context;
        private IHomeService service;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("HomeDB")
                .Options;

            context = new ApplicationDbContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


        }

        [Test]
        public async Task TestGetHomeProductsNumber()
        {
            var repoMock = new Mock<IRepository>();
            List<Banner> banners = new List<Banner>()
            {
                new Banner() { Id = 1, Title="", ImageUrl="" },
                new Banner() { Id = 3, Title="", ImageUrl="" },
                new Banner() { Id = 5, Title="", ImageUrl="" },
                new Banner() { Id = 7, Title="", ImageUrl="" },
                new Banner() { Id = 9, Title="", ImageUrl="" },
                new Banner() { Id = 2, Title="", ImageUrl="" }
            };

            repoMock.Setup(r => r.AllReadonly<Banner>())
                .Returns(banners.AsQueryable().BuildMock());

            repo = repoMock.Object;

            service = new HomeService(repo);

            var bannerCollection = await service.GetHomeProductsAsync();

            Assert.AreEqual(5, bannerCollection.Count());


        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
