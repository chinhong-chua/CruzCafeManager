using CafeBackend.Application.Common;
using CafeBackend.Application.Contracts.Persistence;
using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.DeleteCafe
{
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, Guid>
    {
        private readonly ICafeRepository _cafeRepository;
        public DeleteCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }
        public async Task<Guid> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafeToDelete = await _cafeRepository.GetByIdAsync(request.Id);

            if (cafeToDelete == null)
            {
                throw new NotFoundException(nameof(Cafe), request.Id);
            }

            await _cafeRepository.DeleteAsync(cafeToDelete);

            return cafeToDelete.Id;
        }
    }
}
