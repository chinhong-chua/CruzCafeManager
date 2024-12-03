using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.UpdateCafe
{
    public class UpdateCafeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[]? Logo { get; set; } // Optional
        public string Location { get; set; }
    }
}
