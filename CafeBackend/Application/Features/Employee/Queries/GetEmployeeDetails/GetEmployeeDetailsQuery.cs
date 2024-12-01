using MediatR;

namespace CafeBackend.Application.Features.Employee.Queries.GetEmployeeDetails
{
    public record GetEmployeeDetailsQuery(string Id) : IRequest<EmployeeDetailsDto>;

}
