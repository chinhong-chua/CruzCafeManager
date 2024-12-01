using CafeBackend.Application.Common;
using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Application.Features.Cafe.Commands.DeleteCafe;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, string>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<string> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {

            var employeeToDelete = await _employeeRepository.GetByIdAsync(request.Id);

            if (employeeToDelete == null)
            {
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            await _employeeRepository.DeleteAsync(employeeToDelete);

            return employeeToDelete.Id;
        }
    }
}
