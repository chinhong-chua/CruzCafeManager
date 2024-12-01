using CafeBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
            
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
    }
}
