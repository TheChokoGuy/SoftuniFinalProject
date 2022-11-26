﻿using Microsoft.EntityFrameworkCore;
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
            var product = await context.Items.FirstOrDefaultAsync(p => p.Id == 1);

            context.Items.Remove(product);

            context.SaveChanges();  

        }

        

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var entities = await context.Items
                .Include(m => m.Category)
                .ToListAsync();

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
    }
}