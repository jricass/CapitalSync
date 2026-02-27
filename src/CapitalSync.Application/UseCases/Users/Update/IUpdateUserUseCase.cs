using CapitalSync.Application.DTOs.Users.Requests;

namespace CapitalSync.Application.UseCases.Users.Update;

public interface IUpdateUserUseCase
{
    Task Execute(RequestUpdateUserJson request);
}