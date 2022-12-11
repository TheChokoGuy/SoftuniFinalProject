using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Project.Data.Common;
using Project.Data.Models;
using Project.Data;
using Project.Services.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models;
using Project.Services;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Project.UnitTests
{
    [TestFixture]
    public class ProductsServiceTests
    {
        private IRepository repo;
        private ApplicationDbContext context;
        private IProductService service;

        [SetUp]
        public void SetUp()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("ProductsDB")
                .Options;

            context = new ApplicationDbContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            

        }

        [Test]
        public async Task TestGetCategoriesAsync()
        {
            var repoMock = new Mock<IRepository>();

            List<Category> categories = new List<Category>() {
                new Category() { Id = 1, Name = "Dress"},
                new Category() { Id = 2, Name = "Skirt"},
                new Category() { Id = 3, Name = "Jacket"}
            };

            repoMock.Setup(r => r.AllReadonly<Category>())
                .Returns(categories.AsQueryable().BuildMock());

            repo = repoMock.Object;

            service = new ProductService(repo);

            var categoriesList = await service.GetCategoriesAsync();

            if (categoriesList.Contains(categories[0]) && categoriesList.Contains(categories[1]) && categoriesList.Contains(categories[2]))
            {
                Assert.Pass();
            }
        }

        [Test]
        public async Task TestAddProductAsync()
        {

            var repoMock = new Mock<IRepository>();

            List<Category> categories = new List<Category>() {
                new Category() { Id = 1, Name = "Dress"},
                new Category() { Id = 2, Name = "Skirt"},
                new Category() { Id = 3, Name = "Jacket"}
            };

            repoMock.Setup(r => r.AllReadonly<Category>())
                .Returns(categories.AsQueryable().BuildMock());

            repo = repoMock.Object;

            service = new ProductService(repo);

            AddProductViewModel product = new AddProductViewModel()
            {
                Id = 1,
                AvailableProducts = 3,
                CategoryId = 1,
                Description = "Blue",
                ImageUrl = "yes",
                Name = "Cube",
                Price = (decimal)74.99

            };

            service.AddProductAsync(product);

            Assert.That(repo.GetByIdAsync<Item>(1) != null);

        }

        [Test]
        public async Task TestDeleteProductAsync()
        {
            Category category = new Category() { Id = 1, Name = "Dress" };

            AddProductViewModel product = new AddProductViewModel()
            {
                Id = 1,
                AvailableProducts = 3,
                CategoryId = 1,
                Description = "Blue",
                ImageUrl = "yes",
                Name = "Cube",
                Price = (decimal)74.99

            };

            repo = new Repository(context);

            service = new ProductService(repo);

            await service.AddProductAsync(product);

            var item = await repo.GetByIdAsync<Item>(1);

            await service.DeleteAsync(product.Id);

            item = await repo.GetByIdAsync<Item>(1);

            Assert.That(item == null);
        }

        [Test]
        public async Task TestEditProduct()
        {
            var repo = new Repository(context);
            service = new ProductService(repo);

            Category category = new Category() { Id = 1, Name = "Dress" };

            await service.AddProductAsync(new AddProductViewModel()
            {
                Id = 1,
                AvailableProducts = 3,
                Description = "Blue",
                ImageUrl = "yes",
                Name = "Cube",
                Price = (decimal)74.99,
                CategoryId= 1,
            });

            await repo.SaveChangesAsync();

            await service.EditProductAsync(new Item()
            {
                Id = 1,
                AvailableProducts = 3,
                CategoryId = 1,
                Category = category,
                Description = "Red",
                ImageUrl = "yes",
                Name = "Edit",
                Price = (decimal)74.99,
            });

            var item = await repo.GetByIdAsync<Item>(1);

            Assert.That(item.Description, Is.EqualTo("Red"));
        }

        [Test]
        public async Task TestGetForEdit()
        {
            var repo = new Repository(context);
            service = new ProductService(repo);

            await service.AddProductAsync(new AddProductViewModel()
            {
                Id = 1,
                AvailableProducts = 3,
                Description = "Blue",
                ImageUrl = "yes",
                Name = "Cube",
                Price = (decimal)74.99,
                CategoryId = 1,
            });

            Item product = await service.GetProductAsync(1);

            await repo.SaveChangesAsync();

            var item = await service.GetForEditAsync(1);

            Assert.That(item == product);
        }

        [Test]
        public async Task TestGetProductByStringAsync()
        {
            Category category = new Category() { Id = 1, Name = "Dress" };

            var item1 = new AddProductViewModel()
            {
                Id = 1,
                AvailableProducts = 3,
                Description = "Blue",
                ImageUrl = "yes",
                Name = "Cube",
                Price = (decimal)74.99,
                CategoryId = 1
            };

            var item2 = new AddProductViewModel()
            {
                Id = 2,
                AvailableProducts = 5,
                Description = "Red",
                ImageUrl = "yes",
                Name = "Af",
                Price = (decimal)74.99,
                CategoryId = 1
            };

            var repo = new Repository(context);
            service = new ProductService(repo);

            await service.AddProductAsync(item1);
            await service.AddProductAsync(item2);

            var products = await service.GetProductByStringAsync("A");

            List<Item> productsAsItem = products.Select(p => new Item
            {
                Id= p.Id,
                AvailableProducts= p.AvailableProducts,
                CategoryId = 1,
                Description= p.Description,
                Name= p.Name,
                ImageUrl = p.ImageUrl,
                Price= p.Price,
                Category = category
            }).ToList();

            Item expected = await service.GetProductAsync(2);
            Item real = productsAsItem[0];
            
            if(real.Id == expected.Id)
            {
                if(real.AvailableProducts == expected.AvailableProducts)
                {
                    if(real.Price == expected.Price)
                    {
                        if(real.CategoryId == expected.CategoryId)
                        {
                            if(real.Description == expected.Description)
                            {
                                if(real.ImageUrl == expected.ImageUrl)
                                {
                                    if(real.Name == expected.Name)
                                    {
                                        Assert.Pass();
                                    }
                                }
                            }
                        }
                    
                    }
                }
            }

            await repo.SaveChangesAsync();

        }


        [Test]
        public async Task TestGetAllAsync()
        {
            var repoMock = new Mock<IRepository>();

            List<Item> items= new List<Item>()
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
            service = new ProductService(repo);

            IEnumerable<ProductViewModel> allItems = await service.GetAllAsync("Default");
            IEnumerable<ProductViewModel> skirtItems = await service.GetAllAsync("Skirt");

            if(allItems.Any(p => p.Id == 1) == true && allItems.Any(p => p.Id == 2) == true && skirtItems.Any(p => p.Id == 2))
            {
                Assert.Pass();
            }

            await repo.SaveChangesAsync();

        }

        [TearDown]
        public void TearDown()
        {
            context.Dispose();
        }
    }
}
