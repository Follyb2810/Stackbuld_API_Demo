using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
        {
            var product = new ProductModel
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity
            };

            await _productRepository.CreateAsync(product);
            await _productRepository.SaveChangesAsync();

            return new ProductResponseDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.StockQuantity
            );
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var items = await _productRepository.GetAllAsync();
            var result = new List<ProductResponseDto>();

            foreach (var p in items)
            {
                result.Add(new ProductResponseDto(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.StockQuantity
                ));
            }

            return result;
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var p = await _productRepository.GetByIdAsync(id);
            if (p == null) return null;

            return new ProductResponseDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price,
                p.StockQuantity
            );
        }

        public async Task<ProductResponseDto?> UpdateByIdAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;

            if (!string.IsNullOrWhiteSpace(dto.Name))
                product.Name = dto.Name;

            if (dto.Description is not null)
                product.Description = dto.Description;

            if (dto.Price.HasValue)
                product.Price = dto.Price.Value;

            if (dto.StockQuantity.HasValue)
                product.StockQuantity = dto.StockQuantity.Value;

            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();

            return new ProductResponseDto(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.StockQuantity
            );
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return false;

            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();

            return true;
        }
    }
}
