using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Project.Data.Common;
using Project.Data.Models;
using Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Services.Banner;
using Project.Models;
using Project.Services;

namespace Project.UnitTests
{
    [TestFixture]
    public class BannerServiceTests
    {
        private IRepository repo;
        private ApplicationDbContext context;
        private IBannerService service;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BannerDB")
                .Options;

            context = new ApplicationDbContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


        }

        [Test]
        public async Task TestGetAllAsync()
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

            service = new BannerService(repo);

            var bannerCollection = await service.GetAllAsync();

            Assert.AreEqual(5, bannerCollection.Count());


        }

        [Test]
        public async Task TestGetForEditAsync()
        {
            var repo = new Repository(context);
            service = new BannerService(repo);

            Banner banner = new Banner()
            {
                Id = 1,
                Title = "Cube",
                ImageUrl = "yes"
            };

            await service.AddBannerAsync(banner);

            await repo.SaveChangesAsync();

            var item = await service.GetBannerAsync(1);

            Assert.That(item == banner);


        }

        [Test]
        public async Task TestGetBannerAsync()
        {
            var repo = new Repository(context);
            service = new BannerService(repo);

            await service.AddBannerAsync(new Banner()
            {
                Id = 1,
                Title = "Cube",
                ImageUrl = "yes"
            });

            Banner banner = await service.GetBannerAsync(1);

            await repo.SaveChangesAsync();

            var item = await repo.GetByIdAsync<Banner>(1);

            Assert.That(item == banner);
        }

        [Test]
        public async Task TestDeleteBannerAsync()
        {
            Banner banner = new Banner()
            {
                Id = 1,
                Title = "Cube",
                ImageUrl = "yes"
            };

            repo = new Repository(context);

            service = new BannerService(repo);

            await service.AddBannerAsync(banner);

            var item = await service.GetBannerAsync(banner.Id);

            await service.DeleteAsync(banner.Id);

            item = await service.GetBannerAsync(1);

            Assert.That(item == null);
        }

        [Test]
        public async Task TestEditProduct()
        {
            var repo = new Repository(context);
            service = new BannerService(repo);


            await service.AddBannerAsync(new Banner()
            {
                Id = 1,
                Title = "Cube",
                ImageUrl = "yes"
            });

            await repo.SaveChangesAsync();

            await service.EditBannerAsync(new Banner()
            {
                Id = 1,
                Title = "Circle",
                ImageUrl = "yes"
            });

            await repo.SaveChangesAsync();

            var item = await service.GetBannerAsync(1);

            Assert.That(item.Title, Is.EqualTo("Circle"));
        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
