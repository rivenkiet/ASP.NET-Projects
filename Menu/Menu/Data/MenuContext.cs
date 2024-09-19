using Microsoft.EntityFrameworkCore;
using Menu.Models;

namespace Menu.Data
{
    public class MenuContext : DbContext
    {
        public MenuContext( DbContextOptions<MenuContext> options ) : base(options)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>().HasKey(di => new
            {
                di.DishId,
                di.IngredientId,
            });
            modelBuilder.Entity<DishIngredient>().HasOne(d => d.Dish)
                .WithMany(di => di.DishIngredients)
                .HasForeignKey(d => d.DishId);
            modelBuilder.Entity<DishIngredient>().HasOne(i => i.Ingredient)
                .WithMany(di => di.DishIngredients)
                .HasForeignKey(i => i.IngredientId);

            modelBuilder.Entity<Dish>().HasData(
                new Dish { Id = 1, Name = "Beef Pho", Price = 2, ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9" +
                "/99/Ph%E1%BB%9F_b%C3%B2%2C_C%E1%BA%A7u_Gi%E1%BA%A5y%2C_H%C3%A0_N%E1%BB%99i.jpg/375px-Ph%E1%BB" +
                "%9F_b%C3%B2%2C_C%E1%BA%A7u_Gi%E1%BA%A5y%2C_H%C3%A0_N%E1%BB%99i.jpg" }
                );
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { Id = 1,Name = "Beef"},
                new Ingredient { Id = 2, Name = "Pho"}
                );
            modelBuilder.Entity<DishIngredient>().HasData(
                new DishIngredient { DishId = 1, IngredientId = 1},
                new DishIngredient { DishId =1, IngredientId = 2 }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
    }
}
