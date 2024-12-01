using CafeBackend.Application.Features.Cafe.Queries.GetAllCafes;

namespace CafeBackend.Application.Features.Employee.Queries.GetAllEmployees
{
    public class EmployeeDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public int DaysWorked { get; set; }
        public string? Cafe { get; set; }
    }
}
