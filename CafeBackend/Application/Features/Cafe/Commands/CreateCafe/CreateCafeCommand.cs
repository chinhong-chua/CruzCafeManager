using MediatR;

namespace CafeBackend.Application.Features.Cafe.Commands.CreateCafe
{
    public class CreateCafeCommand : IRequest <Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; } // Optional
        public string Location { get; set; }
    }
}
