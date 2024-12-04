using CafeBackend.Application.Common;
using CafeBackend.Application.Contracts.Persistence;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employeeToUpdate = await _employeeRepository.GetByIdAsync(request.Id);
            if (employeeToUpdate == null)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            employeeToUpdate.Name = request.Name;
            employeeToUpdate.EmailAddress = request.EmailAddress;
            employeeToUpdate.PhoneNumber = request.PhoneNumber;
            employeeToUpdate.CafeId = string.IsNullOrEmpty(request.Cafe) ? null : new Guid(request.Cafe);
            employeeToUpdate.StartDate = request.StartDate;


            await _employeeRepository.UpdateAsync(employeeToUpdate);

            return employeeToUpdate.Id;
        }
    }
}
