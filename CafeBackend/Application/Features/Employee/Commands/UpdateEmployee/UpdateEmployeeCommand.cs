using CafeBackend.Domain.Entities;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string? Cafe { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
