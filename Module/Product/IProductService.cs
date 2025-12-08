using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Product
{
    public interface IProductService
    {
        Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
        Task<ProductResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();
        Task<ProductResponseDto?> UpdateByIdAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteByIdAsync(int id);
    }
}