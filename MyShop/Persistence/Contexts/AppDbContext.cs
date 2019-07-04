using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Item> Items { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Order>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });



            builder.Entity<Role>().HasData(
                    Enum.GetValues(typeof(ERole))
                        .AsQueryable()
                        .Cast<ERole>()
                        .Select(r => new Role
                        {
                            Id = (int) r,
                            Name = r.ToString()
                        }).ToList()
            );

            builder.Entity<Category>().HasData
            (
                new Category { Id = 100, Name = "Fruits and Vegetables" }, // Id set manually due to in-memory provider
                new Category { Id = 101, Name = "Dairy" }
            );

            builder.Entity<Product>().HasData
            (
                new Product
                {
                    Id = 100,
                    Name = "Apple",
                    QuantityInPackage = 1,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    CategoryId = 100
                },
                new Product
                {
                    Id = 101,
                    Name = "Milk",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = EUnitOfMeasurement.Liter,
                    CategoryId = 101,
                }
            );
            
            base.OnModelCreating(builder);
        }


    }
}
