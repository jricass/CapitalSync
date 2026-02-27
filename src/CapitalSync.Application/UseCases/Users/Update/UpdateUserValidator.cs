using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Exception;
using FluentValidation;

namespace CapitalSync.Application.UseCases.Users.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.FirstName).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.LastName).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);
        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.EMAIL_EMPTY)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorMessages.EMAIL_INVALID);
    }
}