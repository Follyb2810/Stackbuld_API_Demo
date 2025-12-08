using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stackbuld_API.Infrastructure;

namespace Stackbuld_API.Module.Order
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<OrderModel?> GetByIdAsync(int id)
        {
            return await _db.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            // return await _db.Orders
            //     .Include(o => o.Items)
            //     .ToListAsync();
            return await _db.Orders.Include(o => o.Items).ToListAsync();
        }

        public async Task CreateAsync(OrderModel order)
        {
            await _db.Orders.AddAsync(order);
        }

        public void Update(OrderModel order)
        {
            _db.Orders.Update(order);
        }

        public void Delete(OrderModel order)
        {
            _db.Orders.Remove(order);
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }
    }
}