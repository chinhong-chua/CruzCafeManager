using CafeBackend.Domain.Entities;

namespace CafeBackend.Application.Contracts.Persistence
{
    public interface IGenericRepository <T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync();
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
