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

            byte[] logoBytes = null;

            // Convert the Base64 string to a byte array
            if (!string.IsNullOrEmpty(request.Logo))
            {
                logoBytes = ConvertBase64ToByteArray(request.Logo);
            }


            var cafeDetails = new Domain.Entities.Cafe()
            {
                Name = request.Name,    
                Description = request.Description,
                Location = request.Location,
                Logo = logoBytes,
            };

            await _cafeRepository.CreateAsync(cafeDetails);

            return cafeDetails.Id;
        }

        private byte[] ConvertBase64ToByteArray(string base64String)
        {
            // Remove the data:image/...;base64, prefix if it exists
            var data = base64String.Contains(",") ? base64String.Split(',')[1] : base64String;
            return Convert.FromBase64String(data);
        }
    }
}
