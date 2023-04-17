using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationLayerAssemblyReference).Assembly);
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ApplicationLayerAssemblyReference>());
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationLayerAssemblyReference));

        return services;
    }
}