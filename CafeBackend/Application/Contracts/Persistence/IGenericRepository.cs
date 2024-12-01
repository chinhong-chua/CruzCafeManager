using CafeBackend.Domain.Entities;

namespace CafeBackend.Application.Contracts.Persistence
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
