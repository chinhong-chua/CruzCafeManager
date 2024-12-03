using CafeBackend.Domain.Entities;

namespace CafeBackend.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed Cafes
            if (!context.Cafes.Any())
            {
                context.Cafes.AddRange(
                    new Cafe
                    {
                        Id = Guid.NewGuid(),
                        Name = "Cruz Cafe",
                        Description = "Description A",
                        Location = "Bugis",
                        Employees = new List<Employee>(),
                        Logo = null
                    },
                    new Cafe
                    {
                        Id = Guid.NewGuid(),
                        Name = "Horizon Cafe",
                        Description = "Description B",
                        Location = "Kallang",
                        Employees = new List<Employee>()
                    },
                    new Cafe
                    {
                        Id = Guid.NewGuid(),
                        Name = "Alice Cafe",
                        Description = "Description C",
                        Location = "Jurong East",
                        Employees = new List<Employee>()
                    }
                );

                await context.SaveChangesAsync();
            }

            // Seed Employees
            if (!context.Employees.Any())
            {
                var cafe = context.Cafes.First();

                context.Employees.AddRange(
                    new Employee
                    {
                        Id = "UI0000001",
                        Name = "Alan Lee",
                        EmailAddress = "alan.lee@test.com",
                        PhoneNumber = "91234567",
                        Gender = Gender.Male,
                        CafeId = cafe.Id,
                        StartDate = DateTime.UtcNow.AddDays(-10)
                    },
                    new Employee
                    {
                        Id = "UI0000002",
                        Name = "Joseph King",
                        EmailAddress = "joseph@test.com",
                        PhoneNumber = "81234567",
                        Gender = Gender.PreferNotToSay,
                        CafeId = cafe.Id,
                    },
                    new Employee
                    {
                        Id = "UI0000003",
                        Name = "Mary Tan",
                        EmailAddress = "mary.tan@test.com",
                        PhoneNumber = "81234568",
                        Gender = Gender.Female,
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
