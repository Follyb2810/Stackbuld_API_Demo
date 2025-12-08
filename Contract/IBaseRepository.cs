using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stackbuld_API.Contract
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(int id);
        Task<int> SaveChangesAsync();
    }

}