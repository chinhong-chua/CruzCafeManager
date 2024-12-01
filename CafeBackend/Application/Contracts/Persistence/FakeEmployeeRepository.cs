using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeBackend.Application.Contracts.Persistence
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees;

        public FakeEmployeeRepository()
        {
            // Initialize with sample data
            _employees = new List<Employee>
            {
                new Employee
                {
                    Id = "UI1234567",
                    EmailAddress = "alice@example.com",
                    PhoneNumber = "123456789",
                    Gender = Gender.Female,
                    CafeId = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow.AddDays(-10)
                },
                new Employee
                {
                    Id = "UI2345678",
                    EmailAddress = "bob@example.com",
                    PhoneNumber = "987654321",
                    Gender = Gender.Male,
                    CafeId = Guid.NewGuid(),
                    StartDate = DateTime.UtcNow.AddDays(-20)
                },
                new Employee
                {
                    Id = "UI3456789",
                    EmailAddress = "charlie@example.com",
                    PhoneNumber = "456123789",
                    Gender = Gender.PreferNotToSay,
                    CafeId = null,
                    StartDate = DateTime.UtcNow.AddDays(-5)
                }
            };
        }

        public Task<Employee> CreateAsync(Employee entity)
        {
            _employees.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<Employee> DeleteAsync(Employee entity)
        {
            _employees.Remove(entity);
            return Task.FromResult(entity);
        }

        public Task<List<Employee>> GetAllAsync()
        {
            return Task.FromResult(_employees);
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            // Since Id is a string, we can find an employee by their unique identifier
            var employee = _employees.FirstOrDefault(e => e.Id == id.ToString());
            return Task.FromResult(employee);
        }

        public Task<Employee> UpdateAsync(Employee entity)
        {
            var existing = _employees.FirstOrDefault(e => e.Id == entity.Id);
            if (existing != null)
            {
                existing.EmailAddress = entity.EmailAddress;
                existing.PhoneNumber = entity.PhoneNumber;
                existing.Gender = entity.Gender;
                existing.CafeId = entity.CafeId;
                existing.StartDate = entity.StartDate;
            }
            return Task.FromResult(existing);
        }
    }
}
