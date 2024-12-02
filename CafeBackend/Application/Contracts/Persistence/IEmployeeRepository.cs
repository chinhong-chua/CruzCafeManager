using CafeBackend.Domain.Entities;

namespace CafeBackend.Application.Contracts.Persistence
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<Employee> GetByIdAsync(string id);
        Task<string> GenerateEmployeeIdAsync();
    }
}
