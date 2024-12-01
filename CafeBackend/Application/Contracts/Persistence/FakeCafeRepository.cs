using CafeBackend.Domain.Entities;

namespace CafeBackend.Application.Contracts.Persistence
{
    public class FakeCafeRepository : ICafeRepository
    {
        public Task<Cafe> CreateAsync(Cafe entity)
        {
            throw new NotImplementedException();
        }

        public Task<Cafe> DeleteAsync(Cafe entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cafe>> GetAllAsync()
        {
            var cafes = new List<Cafe>
        {
            new Cafe
            {
                Id = Guid.NewGuid(),
                Name = "Cafe A",
                Location = "Downtown",
                Description = "Best coffee in town",
                Employees = new List<Employee>
                {
                    new Employee { Id = GenerateUniqueId(), Name = "Alice", EmailAddress = "aaa@a.com", PhoneNumber ="12345" },
                    new Employee { Id = GenerateUniqueId(), Name = "Bob", EmailAddress = "bbb@a.com", PhoneNumber = "12345666" }
                }
            },
            new Cafe
            {
                Id = Guid.NewGuid(),
                Name = "Cafe B",
                Location = "Midtown",
                Description = "Great ambiance",
                Employees = new List<Employee>()
            }
        };

            return Task.FromResult(cafes);
        }

        public Task<Cafe> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Cafe> UpdateAsync(Cafe entity)
        {
            throw new NotImplementedException();
        }

        private string GenerateUniqueId()
        {
            const string prefix = "UI";
            string guidPart = Guid.NewGuid().ToString("N").Substring(0, 7).ToUpper();
            return $"{prefix}{guidPart}";
        }
    }
}
