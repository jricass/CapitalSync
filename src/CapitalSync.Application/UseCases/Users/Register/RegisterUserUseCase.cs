using AutoMapper;
using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Domain.Repositories;
using CapitalSync.Domain.Repositories.Users;
using CapitalSync.Domain.Security.Cryptography;
using CapitalSync.Domain.Security.Tokens;
using CapitalSync.Exception;
using CapitalSync.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace CapitalSync.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypt _passwordEncrypt;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public RegisterUserUseCase(
        IUserRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncrypt passwordEncrypt,
        IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncrypt = passwordEncrypt;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);

        user.Password = _passwordEncrypt.Encrypt(request.Password);

        await _repository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserRole = user.UserRole,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExists = await _repository.ExistActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}