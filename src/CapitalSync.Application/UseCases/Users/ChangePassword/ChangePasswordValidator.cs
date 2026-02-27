using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.UseCases.Login.DoLogin;
using FluentValidation;

namespace CapitalSync.Application.UseCases.Users.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
    }
}