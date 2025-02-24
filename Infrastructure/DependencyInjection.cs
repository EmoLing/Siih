using Core.Interfaces.Repositories.Departments;
using Core.Interfaces.Repositories.Equipment;
using Core.Interfaces.Repositories.Users;
using Infrastructure.Repositories.Departments;
using Infrastructure.Repositories.Equipment;
using Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationDBContext>(o
            => o.UseNpgsql(connectionString).LogTo(Console.WriteLine, LogLevel.Warning).EnableServiceProviderCaching());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJobTitleRepository, JobTitleRepository>();

        services.AddScoped<ICabinetRepository, CabinetRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();

        services.AddScoped<ISoftwareRepository, SoftwareRepository>();
        services.AddScoped<IHardwareRepository, HardwareRepository>();
        services.AddScoped<IComplexHardwareRepository, ComplexHardwareRepository>();

        return services;
    }
}
