using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Application.Features.Cafe.Queries.GetCafeDetails;
using CafeBackend.Application.Features.Employee.Queries.GetAllEmployees;
using CafeBackend.Domain.Entities;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Queries.GetEmployeeDetails
{
    public class GetEmployeeDetailsQueryHandler : IRequestHandler<GetEmployeeDetailsQuery, EmployeeDetailsDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeeDetailsQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<EmployeeDetailsDto> Handle(GetEmployeeDetailsQuery request, CancellationToken cancellationToken)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(request.Id);

            var employeeDetailsDto = new EmployeeDetailsDto()
            {
                Id = employeeDetails.Id,
                Name = employeeDetails.Name!,
                EmailAddress = employeeDetails.EmailAddress,
                PhoneNumber = employeeDetails.PhoneNumber ?? string.Empty,
                DaysWorked = (int)(DateTime.Now - employeeDetails.StartDate.GetValueOrDefault()).TotalDays,
                Cafe = employeeDetails.Cafe?.Name,

            };

            return employeeDetailsDto;
        }
    }
}
