using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Domain.Entities;
using CafeBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {

        }
        public override async Task<IList<Employee>> GetAllAsync()
        {
            return await _context.Employees
                .Include(e => e.Cafe)
                .ToListAsync();
        }
        public async Task<Employee> GetByIdAsync(string id)
        {
            var employee = await _context.Employees
                .Include(e => e.Cafe)
                .FirstOrDefaultAsync(e => e.Id == id);
            return employee;
        }

        public async Task<string> GenerateEmployeeIdAsync()
        {
            string newId;
            do
            {
                newId = "UI" + GetRandomAlphaNumericString(7);
            }
            while (await _context.Employees.AnyAsync(e => e.Id == newId));

            return newId;
        }


        private string GetRandomAlphaNumericString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
