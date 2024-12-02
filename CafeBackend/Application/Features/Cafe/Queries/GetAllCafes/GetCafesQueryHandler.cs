using CafeBackend.Application.Contracts.Persistence;
using MediatR;

namespace CafeBackend.Application.Features.Cafe.Queries.GetAllCafes
{
    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, List<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;
        public GetCafesQueryHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task<List<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeRepository.GetAllAsync();

            if(!String.IsNullOrEmpty(request.Location))
            {
                cafes = cafes.Where(c => c.Location == request.Location).ToList();
            }

            var cafesDto = cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name!,
                Description = c.Description,
                Location = c.Location,
                Employees = c.Employees?.Count > 0 ? c.Employees.Count : 0,
                Logo = null
            }).ToList();

            return cafesDto;

        }
    }
}
