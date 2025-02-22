using Core.Services;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.Mappings;

namespace IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connectionString)
    {
        services.AddInfrastructure(connectionString);

        services.AddScoped<UserService>();
        services.AddScoped<JobTitleService>();
        services.AddScoped<CabinetService>();
        services.AddScoped<DepartmentService>();
        services.AddScoped<SoftwareService>();
        services.AddScoped<HardwareService>();
        services.AddScoped<ComplexHardwareService>();

        return services;
    }

    public static IServiceCollection AddAutoMapperProfilesServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddAutoMapper(typeof(JobTitleProfile));
        services.AddAutoMapper(typeof(CabinetProfile));
        services.AddAutoMapper(typeof(DepartmentProfile));
        services.AddAutoMapper(typeof(HardwareProfile));
        services.AddAutoMapper(typeof(SoftwareProfile));
        services.AddAutoMapper(typeof(ComplexHardwareProfile));
        services.AddAutoMapper(typeof(ComplexHardwareTypeProfile));

        return services;
    }
}
