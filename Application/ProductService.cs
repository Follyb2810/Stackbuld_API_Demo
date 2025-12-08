public interface IProductService
{
    Task<ProductDto> CreateAsync(CreateProductDto input);
    Task<ProductDto?> GetAsync(int id);
    Task<List<ProductDto>> ListAsync();
    Task<ProductDto?> UpdateAsync(int id, UpdateProductDto input);
    Task<bool> DeleteAsync(int id);
}

public class ProductService : IProductService
{
    private readonly AppDbContext _db;
    public ProductService(AppDbContext db) { _db = db; }

    public async Task<ProductDto> CreateAsync(CreateProductDto input)
    {
        var p = new Product { Name = input.Name, Description = input.Description, Price = input.Price, StockQuantity = input.StockQuantity };
        _db.Products.Add(p);
        await _db.SaveChangesAsync();
        return new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
    }

    public async Task<ProductDto?> GetAsync(int id)
    {
        var p = await _db.Products.FindAsync(id);
        return p == null ? null : new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
    }

    public async Task<List<ProductDto>> ListAsync()
    {
        return await _db.Products.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity)).ToListAsync();
    }

    public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto input)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return null;
        if (input.Name is not null) p.Name = input.Name;
        if (input.Description is not null) p.Description = input.Description;
        if (input.Price.HasValue) p.Price = input.Price.Value;
        if (input.StockQuantity.HasValue) p.StockQuantity = input.StockQuantity.Value;
        await _db.SaveChangesAsync();
        return new ProductDto(p.Id, p.Name, p.Description, p.Price, p.StockQuantity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return false;
        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        return true;
    }
}
