using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.UseCases.Login.DoLogin;
using CapitalSync.Exception;
using FluentValidation;

namespace CapitalSync.Application.UseCases.Users.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY);

        RuleFor(user => user.LastName)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.LASTNAME_EMPTY);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}