using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Domain.Repositories;
using CapitalSync.Domain.Repositories.Users;
using CapitalSync.Domain.Security.Cryptography;
using CapitalSync.Domain.Services.LoggedUser;
using CapitalSync.Exception;
using CapitalSync.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CapitalSync.Application.UseCases.Users.ChangePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncrypt _passwordEncrypt;

    public ChangePasswordUseCase (
        ILoggedUser loggedUser,
        IUserRepository repository,
        IUnitOfWork unitOfWork,
        IPasswordEncrypt passwordEncrypt)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _passwordEncrypt = passwordEncrypt;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        
    }

    private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
    {
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        var passwordMatch = _passwordEncrypt.Verify(request.Password, loggedUser.Password);

        if (passwordMatch == false)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.INVALID_CREDENTIALS));
        }

        if (result.IsValid == false)
        {
            var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}