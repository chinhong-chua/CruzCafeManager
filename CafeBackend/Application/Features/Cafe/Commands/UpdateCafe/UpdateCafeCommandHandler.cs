using CafeBackend.Application.Contracts.Persistence;
using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.UpdateCafe
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, Guid>
    {
        private ICafeRepository _cafeRepository;
        public UpdateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task<Guid> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafeToUpdate = new Domain.Entities.Cafe()
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Logo = request.Logo,
            };
            await _cafeRepository.UpdateAsync(cafeToUpdate);

            return cafeToUpdate.Id;
        }
    }
}
