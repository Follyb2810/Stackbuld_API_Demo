using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stackbuld_API.Module.Order;
using Stackbuld_API.Module.Product;

namespace Stackbuld_API.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductModel> Products => Set<ProductModel>();
        public DbSet<OrderModel> Orders => Set<OrderModel>();
        public DbSet<OrderItemModel> OrderItems => Set<OrderItemModel>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>().Property(p => p.Price).HasPrecision(22, 5);
            // modelBuilder.Entity<ProductModel>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderModel>().HasMany(o => o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}