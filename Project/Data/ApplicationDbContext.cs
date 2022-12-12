using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Project.Data.Models;

namespace Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Banner> Banners { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>()
                .HasData(new Category()
                {
                    Id = 1,
                    Name = "Dress"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Skirt"
                },
                new Category()
                {
                    Id = 3,
                    Name = "T-Shirt"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Jeans"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Shoes"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}