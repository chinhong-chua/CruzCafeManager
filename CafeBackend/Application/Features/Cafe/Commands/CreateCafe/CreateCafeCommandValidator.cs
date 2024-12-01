using FluentValidation;
using System.Data;

namespace CafeBackend.Application.Features.Cafe.Commands.CreateCafe
{
    public class CreateCafeCommandValidator : AbstractValidator<CreateCafeCommand>
    {
        public CreateCafeCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(80).WithMessage("{PropertyName} must be less than length 80.");

            RuleFor(c => c.Description)
             .NotEmpty()
             .MinimumLength(5).WithMessage("{PropertyName} must be at least length 5.");

            RuleFor(c => c.Location)
             .NotEmpty();

        }
    }
}
