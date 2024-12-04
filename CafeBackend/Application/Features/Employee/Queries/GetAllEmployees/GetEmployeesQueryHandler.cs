using CafeBackend.Application.Common;
using CafeBackend.Application.Contracts.Persistence;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Queries.GetAllEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeesQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {

            var employees = await _employeeRepository.GetAllAsync();

            if (!String.IsNullOrEmpty(request.CafeName))
                employees = employees.Where(e => string.Equals(e.Cafe?.Name, request.CafeName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!employees.Any())
                return new List<EmployeeDto>();

            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name!,
                EmailAddress = e.EmailAddress,
                PhoneNumber = e.PhoneNumber ?? string.Empty,
                DaysWorked = e.StartDate == null? 0 : (int)(DateTime.Now - e.StartDate.Value).TotalDays,
                Cafe = e.Cafe?.Name,

            }).ToList();

            return employeeDtos;

        }
    }
}
