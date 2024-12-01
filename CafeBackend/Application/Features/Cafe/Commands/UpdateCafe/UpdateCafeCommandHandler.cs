using CafeBackend.Application.Common;
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
            var cafeToUpdate = await _cafeRepository.GetByIdAsync(request.Id);
            if (cafeToUpdate == null)
            {
                throw new NotFoundException(nameof(Cafe),request.Id);
            }

            cafeToUpdate.Name = request.Name;
            cafeToUpdate.Description = request.Description;
            cafeToUpdate.Location = request.Location;
            cafeToUpdate.Logo = request.Logo;

            await _cafeRepository.UpdateAsync(cafeToUpdate);

            return cafeToUpdate.Id;
        }
    }
}
