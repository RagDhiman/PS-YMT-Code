using AM_BackendForFrontend_Data.Generic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AM_BackendForFrontend_Data.Data
{
    public interface IGenericRepository<T> where T : IEntity
    {
        string ResourcePath { get; set; }
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<bool> StoreNewAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
