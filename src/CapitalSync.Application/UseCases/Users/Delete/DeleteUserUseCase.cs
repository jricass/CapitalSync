using CapitalSync.Domain.Repositories;
using CapitalSync.Domain.Repositories.Users;
using CapitalSync.Domain.Services.LoggedUser;

namespace CapitalSync.Application.UseCases.Users.Delete;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserUseCase(ILoggedUser loggedUser, IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        user.Deactivate();

        _repository.Update(user);

        await _unitOfWork.Commit();
    }
}