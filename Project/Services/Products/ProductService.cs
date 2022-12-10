using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Data.Models;
using Project.Models;

namespace Project.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddProductAsync(AddProductViewModel model)
        {
            Item item = new Item()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Description = model.Description,
                AvailableProducts = model.AvailableProducts,
                Price = model.Price,
                CategoryId = model.CategoryId
            };

            await context.Items.AddAsync(item);
            
            await context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int productId)
        {
            var product = await context.Items.FirstOrDefaultAsync(p => p.Id == productId);

            context.Items.Remove(product);

            context.SaveChanges();  

        }

        

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string type)
        {
            var entities = new List<Item>();

            if(type == "Default")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .ToListAsync();
            }
            else if(type == "Alphabetically")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .OrderBy(m => m.Name)
                    .ToListAsync();
            }
            else if (type == "Newest")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .OrderByDescending(m => m.Id)
                    .ToListAsync();
            }
            else if (type == "Oldest")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .OrderBy(m => m.Id)
                    .ToListAsync();
            }
            else if (type == "Expensive")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .OrderByDescending(m => m.Price)
                    .ToListAsync();
            }
            else if (type == "Cheaper")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .OrderBy(m => m.Price)
                    .ToListAsync();
            }
            else if (type == "Available")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .Where(m => m.AvailableProducts > 0)
                    .ToListAsync();
            }
            else if (type == "Shoes")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .Where(m => m.Category.Name == "Shoes")
                    .ToListAsync();
            }
            else if (type == "Jeans")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .Where(m => m.Category.Name == "Jeans")
                    .ToListAsync();
            }
            else if (type == "T-Shirt")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .Where(m => m.Category.Name == "T-Shirt")
                    .ToListAsync();
            }
            else if (type == "Skirt")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .Where(m => m.Category.Name == "Skirt")
                    .ToListAsync();
            }
            else if (type == "Dress")
            {
                entities = await context.Items
                    .Include(m => m.Category)
                    .Where(m => m.Category.Name == "Dress")
                    .ToListAsync();
            }
            var products = entities
                .Select(p => new ProductViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    AvailableProducts = p.AvailableProducts,
                    Category = p?.Category?.Name,
                    Price = p.Price

                }).ToList();

            return products;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task EditProductAsync(Item model)
        {
            Item item = await context.Items.FindAsync(model.Id);

            context.Items.Update(item);

            item.AvailableProducts = model.AvailableProducts;
            item.Description = model.Description;
            item.CategoryId = model.CategoryId;
            item.Id = model.Id;
            item.ImageUrl = model.ImageUrl;
            item.Name = model.Name;
            item.Price = model.Price;

            await context.SaveChangesAsync();
        }

        public async Task<Item> GetForEditAsync(int productId)
        {
            Item entity = await context.Items.FindAsync(productId);

            entity.Categories = await this.GetCategoriesAsync();

            return entity;

        }

        public async Task<Item> GetProductAsync(int productId)
        {
            Item product = await context.Items.FirstOrDefaultAsync(product => product.Id == productId);

            return product;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByStringAsync(string text)
        {
            var items = await context.Items.Where(s => s.Name!.Contains(text)).Select(i => new ProductViewModel
            {
                Name = i.Name,
                Description = i.Description,
                AvailableProducts = i.AvailableProducts,
                Category = i.Category.Name,
                ImageUrl = i.ImageUrl,
                Id = i.Id,
                Price = i.Price,
            }).ToListAsync();

            return items;
        }
    }
}
