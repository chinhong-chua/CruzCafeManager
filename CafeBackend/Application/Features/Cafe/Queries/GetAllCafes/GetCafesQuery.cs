using CafeBackend.Domain.Entities;
using MediatR;

namespace CafeBackend.Application.Features.Cafe.Queries.GetAllCafes
{

    public record GetCafesQuery : IRequest<List<CafeDto>>
    {
        public string? Location { get; set; }
    }
}
