using FileManager.Domain.BlogPostAttachments;
using FileManager.Domain.Entities;
using FileManager.Domain.Files.ProductFiles;
using FileManager.Domain.Files.Repositories;
using FileManager.Domain.ProfileImages;
using FileManager.Persistence.Interceptors;
using FileManager.Persistence.Repositories;
using FileManager.Persistence.Repositories.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Persistence;
public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddSingleton<SoftDeletedInterceptor>();
        services.AddSingleton<CreatedInterceptor>();
        services.AddSingleton<ModifiedInterceptor>();

        services.AddDbContextFactory<ApplicationDbContext>(
            (sp, option) => option
            .UseLazyLoadingProxies()
            .UseSqlServer(connectionString)
            .AddInterceptors(sp.GetRequiredService<SoftDeletedInterceptor>())
            .AddInterceptors(sp.GetRequiredService<CreatedInterceptor>())
            .AddInterceptors(sp.GetRequiredService<ModifiedInterceptor>())
            );

        //services.AddDbContextFactory<ApplicationDbContext>(options => 
        //    options.UseLazyLoadingProxies()
        //   .UseSqlServer(connectionString));

        #region Repositories
        services.AddScoped<IFileItemRepository, FileItemRepository>();
        services.AddScoped<IProfileImageRepository, ProfileImageRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IBlogPostAttachmentRepository, BlogPostAttachmentRepository>();
        #endregion

        #region Files Repositories
        services.AddScoped<ILocalFileRepository, LocalFileRepository>();
        services.AddScoped<IDatabaseFileRepository, DatabaseFileRepository>();
        services.AddScoped<ICloudFileRepository, CloudFileRepository>();
        #endregion

        return services;
    }
}