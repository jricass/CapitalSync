using CapitalSync.Application.DTOs.Users.Responses;

namespace CapitalSync.Application.UseCases.Users.GetProfile;

public interface IGetUserProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}