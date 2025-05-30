using FileManager.Application.FileServices;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Application;
public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {


        services.AddScoped<IFileService, FileService>();


        return services;
    }
}