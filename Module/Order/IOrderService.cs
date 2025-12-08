using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Module.Order
{
    public interface IOrderService
    {

        Task<OrderResponseDto> CreateAsync(OrderDto dto);
        Task<OrderResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetAllAsync();
        Task<OrderResponseDto?> UpdateAsync(int id, OrderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}