using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Database; 
using TaskTracker.Persistence.Context;
using TaskTracker.Persistence.Repositories;
using TaskTracker.Persistence.UnitOfWork;

namespace TaskTracker.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
       
        services.AddDbContext<TaskTrackerDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(TaskTrackerDbContext).Assembly.FullName)));

      
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

       
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

       
        services.AddTransient<DatabaseInitializer>();

        return services;
    }
}
