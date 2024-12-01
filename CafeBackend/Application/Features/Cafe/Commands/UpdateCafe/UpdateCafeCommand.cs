using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.UpdateCafe
{
    public class UpdateCafeCommand : IRequest<Guid>
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public byte[]? Logo { get; set; } // Optional
        public required string Location { get; set; }
    }
}
