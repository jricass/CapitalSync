using AutoMapper;
using CapitalSync.Application.DTOs.Users.Requests;
using CapitalSync.Application.DTOs.Users.Responses;
using CapitalSync.Domain.Entities;

namespace CapitalSync.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Password, config => config.MapFrom(src => src.Password));
    }

    private void EntityToResponse()
    {
        CreateMap<User, ResponseUserProfileJson>();
    }
}