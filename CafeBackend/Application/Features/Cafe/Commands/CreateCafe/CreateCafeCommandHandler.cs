using CafeBackend.Application.Common;
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

            var validator = new CreateCafeCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new BadRequestException("Invalid cafe", validationResult);
            }

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
