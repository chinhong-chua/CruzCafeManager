using FluentValidation;

namespace CafeBackend.Application.Features.Employee.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(80).WithMessage("{PropertyName} must be less than length 80.");

            RuleFor(c => c.EmailAddress)
             .NotEmpty()
             .EmailAddress();

            RuleFor(c => c.PhoneNumber)
             .NotEmpty()
             .Matches(@"^[89]\d{7}$").WithMessage("Phone number must start with 8 or 9 and have exactly 8 digits.");

            RuleFor(c=> c.Gender)
                .NotEmpty();

        }
    }
}
