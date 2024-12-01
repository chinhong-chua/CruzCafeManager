using CafeBackend.Domain.Entities;
using MediatR;

namespace CafeBackend.Application.Features.Employee.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<string>
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string EmailAddress { get; set; }
        public required string PhoneNumber { get; set; }
        public Guid? CafeId { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
