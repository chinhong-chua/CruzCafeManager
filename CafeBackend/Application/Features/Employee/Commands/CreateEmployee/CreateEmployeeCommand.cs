using CafeBackend.Domain.Entities;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommand : IRequest<String>
    {
        public required string Name { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public Gender? Gender { get; set; }
        public string? Cafe { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
