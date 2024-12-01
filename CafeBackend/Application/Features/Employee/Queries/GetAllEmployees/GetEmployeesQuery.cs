using MediatR;

namespace CafeBackend.Application.Features.Employee.Queries.GetAllEmployees
{
    public class GetEmployeesQuery : IRequest<List<EmployeeDto>>
    {
        public string? Name { get; set; }
    }
}
