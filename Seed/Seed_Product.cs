using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stackbuld_API.Module.Product;
using Stackbuld_API.Module.Order;
using Stackbuld_API.Infrastructure;

namespace Stackbuld_API.Seed
{
    public static class Seed_Product
    {
        public static async Task SeedData(AppDbContext db)
        {
            if (!db.Products.Any())
            {
                db.Products.AddRange(
                    new ProductModel { Name = "Laptop", Description = "Gaming Laptop", Price = 1500m, StockQuantity = 10 },
                    new ProductModel { Name = "Phone", Description = "Smartphone", Price = 800m, StockQuantity = 20 },
                    new ProductModel { Name = "Headphones", Description = "Noise Cancelling", Price = 200m, StockQuantity = 30 }
                );
                await db.SaveChangesAsync();
            }

            if (!db.Orders.Any())
            {
                var firstProduct = db.Products.First();

                var order = new OrderModel
                {
                    CreatedAt = DateTime.UtcNow,
                    Items = new List<OrderItemModel>
                    {
                        new OrderItemModel
                        {
                            ProductId = firstProduct.Id,
                            Quantity = 1,
                            UnitPrice = firstProduct.Price
                        }
                    }
                };

                order.TotalAmount = order.Items.Sum(i => i.UnitPrice * i.Quantity);

                db.Orders.Add(order);
                await db.SaveChangesAsync();
            }
        }
    }
}
