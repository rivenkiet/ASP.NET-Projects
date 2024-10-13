﻿// <auto-generated />
using Bulky.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BulkyWeb.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241011153130_addColumn")]
    partial class addColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bulky.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Science"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Action"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "History"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "michos",
                            CategoryId = 1,
                            Description = "wibu time",
                            ISBN = "1",
                            ImageUrl = "",
                            ListPrice = 200.0,
                            Price = 200.0,
                            Price100 = 150.0,
                            Price50 = 180.0,
                            Title = "Genshin Impact"
                        },
                        new
                        {
                            Id = 2,
                            Author = "michos",
                            CategoryId = 1,
                            Description = "wibu time",
                            ISBN = "2",
                            ImageUrl = "",
                            ListPrice = 200.0,
                            Price = 200.0,
                            Price100 = 150.0,
                            Price50 = 180.0,
                            Title = "Honkai Impact"
                        },
                        new
                        {
                            Id = 3,
                            Author = "michos",
                            CategoryId = 2,
                            Description = "wibu time",
                            ISBN = "2",
                            ImageUrl = "",
                            ListPrice = 300.0,
                            Price = 300.0,
                            Price100 = 250.0,
                            Price50 = 280.0,
                            Title = "Honkai SR"
                        },
                        new
                        {
                            Id = 4,
                            Author = "FromSofware",
                            CategoryId = 3,
                            Description = "dark time",
                            ISBN = "10",
                            ImageUrl = "",
                            ListPrice = 1000.0,
                            Price = 1000.0,
                            Price100 = 300.0,
                            Price50 = 500.0,
                            Title = "Dark Soul"
                        });
                });

            modelBuilder.Entity("Bulky.Models.Product", b =>
                {
                    b.HasOne("Bulky.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
