using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Science", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Action", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product {
                    Id = 1,
                    Title="Genshin Impact",
                    Description = "wibu time", 
                    Author ="michos",
                    ISBN = "1",
                    ListPrice = 200,
                    Price = 200,
                    Price50 = 180,
                    Price100 = 150,
                    CategoryId = 1,
					ImageUrl = ""
				},
                new Product { Id = 2,
                    Title = "Honkai Impact",
                    Description = "wibu time",
                    Author = "michos",
                    ISBN = "2",
                    ListPrice = 200,
                    Price = 200,
                    Price50 = 180,
                    Price100 = 150,
					CategoryId = 1,
					ImageUrl = ""
				},
                new Product { Id = 3,
                    Title = "Honkai SR", 
                    Description = "wibu time", 
                    Author = "michos", 
                    ISBN = "2", 
                    ListPrice = 300,
                    Price = 300,
                    Price50 = 280,
                    Price100 = 250,
					CategoryId = 2,
					ImageUrl = ""
				},
                new Product { Id = 4, 
                    Title = "Dark Soul", 
                    Description = "dark time", 
                    Author = "FromSofware",
                    ISBN = "10",
                    ListPrice = 1000,
                    Price = 1000,
                    Price50 = 500,
                    Price100 = 300,
					CategoryId = 3,
					ImageUrl = ""
				}
                );
        }
    }
}
