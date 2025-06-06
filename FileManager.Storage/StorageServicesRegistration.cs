using FileManager.Domain.Services.Infrastructure.Storage;
using FileManager.Storage.FileStorageServices;
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

        #region Services
        services.AddScoped<IFileStorageService1, LocalFileStorageService1>();
        services.AddScoped<ILocalFileStorageService, LocalFileStorageService>();
        services.AddScoped<IDatabaseFileStorageService, DatabaseFileStorageService>();
        services.AddScoped<ICloudFileStorageService, CloudFileStorageService>();
        #endregion

        return services;
    }
}