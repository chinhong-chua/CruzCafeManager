using CafeBackend.Application.Contracts.Persistence;
using CafeBackend.Application.Features.Cafe.Queries.GetCafeDetails;
using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.CreateCafe
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly ICafeRepository _cafeRepository;
        public CreateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafeDetails = new Domain.Entities.Cafe()
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Logo = request.Logo,
            };

            await _cafeRepository.CreateAsync(cafeDetails);

            return cafeDetails.Id;
        }
    }
}
