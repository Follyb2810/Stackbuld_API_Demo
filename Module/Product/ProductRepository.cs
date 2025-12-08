using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stackbuld_API.Infrastructure;

namespace Stackbuld_API.Module.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(ProductModel entity)
        {
            await _db.Products.AddAsync(entity);
        }

        public void Delete(ProductModel entity)
        {
            _db.Products.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<ProductModel?> GetByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }

        public void Update(ProductModel entity)
        {
            _db.Products.Update(entity);
        }
    }
}