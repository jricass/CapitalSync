using AutoMapper;
using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Domain.Repositories;
using CapitalSync.Domain.Repositories.Users;

namespace CapitalSync.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterUserUseCase(
        IUserRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
    {
        var user = _mapper.Map<Domain.Entities.User>(request);

        await _repository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseUserJson
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            UserRole = user.UserRole,
            IsActive = user.IsActive
        };
    }
}