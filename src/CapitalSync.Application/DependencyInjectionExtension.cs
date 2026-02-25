using CapitalSync.Application.AutoMapper;
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
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
    }
}