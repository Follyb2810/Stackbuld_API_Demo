using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Order
{

    public interface IOrderRepository
    {
        Task<OrderModel?> GetByIdAsync(int id);
        Task<IEnumerable<OrderModel>> GetAllAsync();
        Task CreateAsync(OrderModel order);
        void Update(OrderModel order);
        void Delete(OrderModel order);
        Task<int> SaveChangesAsync();


    }
}