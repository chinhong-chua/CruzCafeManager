using CafeBackend.Application.Contracts.Persistence;
using MediatR;

namespace CafeBackend.Application.Features.Cafe.Queries.GetCafeDetails
{
    public class GetCafeDetailsQueryHandler : IRequestHandler<GetCafeDetailsQuery, CafeDetailsDto>
    {
        private readonly ICafeRepository _cafeRepository;
        public GetCafeDetailsQueryHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task<CafeDetailsDto> Handle(GetCafeDetailsQuery request, CancellationToken cancellationToken)
        {
            var cafeDetails = await _cafeRepository.GetByIdAsync(request.Id);

            var cafeDetailsDto = new CafeDetailsDto()
            {
                Id = request.Id,
                Description = cafeDetails.Description,
                Name = cafeDetails.Name,
                Location = cafeDetails.Location,
                Employees = cafeDetails.Employees?.Count() ?? 0,
                Logo = cafeDetails.Logo
            };

            return cafeDetailsDto;
        }
    }
}
