using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Behaviors;
using TaskTracker.Application.Mappings;

namespace TaskTracker.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
       
        var assembly = typeof(UserProfile).Assembly;

      
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

      
        services.AddAutoMapper(assembly);

     
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}