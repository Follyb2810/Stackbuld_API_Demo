using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Product
{
    public record CreateProductDto(string Name, string? Description, decimal Price, int StockQuantity);
    public record UpdateProductDto(string? Name, string? Description, decimal? Price, int? StockQuantity);
    public record ProductDto(int Id, string Name, string? Description, decimal Price, int StockQuantity);
    public record ProductResponseDto(
        int Id,
        string Name,
        string? Description,
        decimal Price,
        int StockQuantity
    );
}