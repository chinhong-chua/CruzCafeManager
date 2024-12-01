using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.CreateCafe
{
    public class CreateCafeCommand : IRequest <Guid>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public byte[]? Logo { get; set; } // Optional
        public required string Location { get; set; }
    }
}
