using CafeBackend.Application.Common;
using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Application.Features.Cafe.Commands.CreateCafe;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEmployeeCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid employee", validationResult);
            }

            var employee = new Domain.Entities.Employee
            {
                Id = await _employeeRepository.GenerateEmployeeIdAsync(),
                Name = request.Name,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = (Domain.Entities.Gender)request.Gender!,
                CafeId = string.IsNullOrEmpty(request.Cafe)? null : new Guid(request.Cafe),
                StartDate = request.StartDate ?? DateTime.UtcNow
            };

            await _employeeRepository.CreateAsync(employee);

            return employee.Id;
        }
    }
}
