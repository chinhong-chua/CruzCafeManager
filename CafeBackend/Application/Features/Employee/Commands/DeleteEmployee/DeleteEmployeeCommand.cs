using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<string>
    {
        public string Id { get; set; }
    }
}
