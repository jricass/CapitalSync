using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Exception;
using FluentValidation;

namespace CapitalSync.Application.UseCases.Login.DoLogin;

public class LoginValidator : AbstractValidator<RequestLoginUserJson>
{
    public LoginValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);

        RuleFor(user => user.Password)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.PASSWORD_EMPTY);
    }
}