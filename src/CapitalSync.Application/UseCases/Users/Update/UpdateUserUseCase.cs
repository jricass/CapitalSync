using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Domain.Repositories;
using CapitalSync.Domain.Repositories.Users;
using CapitalSync.Domain.Services.LoggedUser;
using CapitalSync.Exception;
using CapitalSync.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CapitalSync.Application.UseCases.Users.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(
        ILoggedUser loggedUser,
        IUserRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.Get();

        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if (currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _repository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}