using CapitalSync.Application.DTOs.Users.Requests;

namespace CapitalSync.Application.UseCases.Users.ChangePassword;

public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}