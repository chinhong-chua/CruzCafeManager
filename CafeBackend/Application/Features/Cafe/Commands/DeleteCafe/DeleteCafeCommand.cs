using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.DeleteCafe
{
    public class DeleteCafeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
