using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Domain.Entities;
using CafeBackend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Infrastructure.Repositories
{
    public class CafeRepository : GenericRepository<Cafe>, ICafeRepository
    {
        public CafeRepository(ApplicationDbContext context): base(context) 
        {
        }

        public override async Task<IList<Cafe>> GetAllAsync()
        {
            return await _context.Cafes
                .Include(c=>c.Employees)
                .ToListAsync();
        }
        public async Task<Cafe> GetByIdAsync(Guid id)
        {
            var cafe = await _context.Cafes.FirstOrDefaultAsync(c => c.Id == id);
            return cafe;
        }
    }
}
