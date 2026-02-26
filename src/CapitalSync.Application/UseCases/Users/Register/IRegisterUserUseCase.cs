using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;

namespace CapitalSync.Application.UseCases.Users.Register;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request);
}