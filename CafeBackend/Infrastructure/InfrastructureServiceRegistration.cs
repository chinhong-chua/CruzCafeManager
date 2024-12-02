using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Infrastructure.Persistence;
using CafeBackend.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICafeRepository, FakeCafeRepository>();
            services.AddScoped<IEmployeeRepository, FakeEmployeeRepository>();

            return services;
        }
    }
}
