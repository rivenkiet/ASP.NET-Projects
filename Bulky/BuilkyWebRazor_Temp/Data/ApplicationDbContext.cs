﻿using BuilkyWebRazor_Temp.Models;
using Microsoft.EntityFrameworkCore;

namespace BuilkyWebRazor_Temp.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Category> categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>().HasData(
				new Category { Id = 1, Name = "Science", DisplayOrder = 1 },
				new Category { Id = 2, Name = "Action", DisplayOrder = 2 },
				new Category { Id = 3, Name = "History", DisplayOrder = 3 }
				);
		}
	}
}
