using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}