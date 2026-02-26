using CapitalSync.Application.AutoMapper;
using CapitalSync.Application.UseCases.Login.DoLogin;
using CapitalSync.Application.UseCases.Users.GetProfile;
using CapitalSync.Application.UseCases.Users.Register;
using Microsoft.Extensions.DependencyInjection;

namespace CapitalSync.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile(typeof(AutoMapping)));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
    }
}