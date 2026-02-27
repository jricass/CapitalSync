using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Domain.Repositories.Users;
using CapitalSync.Domain.Security.Cryptography;
using CapitalSync.Domain.Security.Tokens;
using CapitalSync.Exception.ExceptionsBase;

namespace CapitalSync.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserRepository _repository;
    private readonly IPasswordEncrypt _passwordEncrypt;
    private readonly IAccessTokenGenerator _accessTokenGenerator;

    public DoLoginUseCase(IUserRepository repository, IPasswordEncrypt passwordEncrypt, IAccessTokenGenerator accessTokenGenerator)
    {
        _repository = repository;
        _passwordEncrypt = passwordEncrypt;
        _accessTokenGenerator = accessTokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginUserJson request)
    {
        await Validate(request);

        var user = await _repository.GetUserByEmail(request.Email);

        if (user is null)
        {
            throw new InvalidLoginException();
        }

        var passwordMatch = _passwordEncrypt.Verify(request.Password, user.Password);

        if (!passwordMatch)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

    private static async Task Validate(RequestLoginUserJson request)
    {
        var validator = new LoginValidator();
        var result = await validator.ValidateAsync(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}