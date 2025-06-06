using FileManager.Application.FileServices;
using FileManager.Application.ProfileImageServices;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Application;
public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region Services
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IProfileImageService, ProfileImageService>();
        #endregion
        return services;
    }
}