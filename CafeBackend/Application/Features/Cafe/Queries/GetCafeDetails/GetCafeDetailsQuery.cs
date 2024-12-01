using MediatR;

namespace CafeBackend.Application.Features.Cafe.Queries.GetCafeDetails
{
    public record GetCafeDetailsQuery(Guid Id) : IRequest<CafeDetailsDto>;

}
