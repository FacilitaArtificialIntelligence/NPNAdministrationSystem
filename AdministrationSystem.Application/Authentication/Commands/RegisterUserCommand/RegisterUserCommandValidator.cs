using FluentValidation;

namespace AdministrationSystem.Application.Users.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(x => x.FullName)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.CPF)
            .Matches(@"^\d{11}$")
            .WithMessage("CPF must contain 11 digits");

        RuleFor(x => x.Telefone)
            .Matches(@"^\+?55\s?\(?\d{2}\)?\s?(?:9\d{4}|\d{4})-?\d{4}$")
            .WithMessage("Invalid Brazilian phone format");

        RuleFor(x => x.CEP)
            .Matches(@"^\d{5}-?\d{3}$")
            .WithMessage("Invalid Brazilian postal code");
    }
}
