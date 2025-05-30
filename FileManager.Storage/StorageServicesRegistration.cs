using FileManager.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Storage;
public static class StorageServicesRegistration
{
    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        //var connectionString = configuration
        //    .GetConnectionString("DefaultConnection") ??
        //    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


        services.AddScoped<IFileStorageService, PhysicalFileStorageService>();


        return services;
    }
}