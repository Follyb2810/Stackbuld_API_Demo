using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stackbuld_API.Module.Product;

namespace Stackbuld_API.Module.Order
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
        public decimal TotalAmount { get; set; }

    }
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ProductModel? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}