using FileManager.Domain.Entities;
using FileManager.Domain.Files.BlogPostFiles;
using FileManager.Domain.Files.ProductFiles;
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

        //services.AddSingleton<SoftDeletedInterceptor>();
        //services.AddSingleton<CreatedInterceptor>();
        //services.AddSingleton<ModifiedInterceptor>();

        //services.AddDbContextFactory<ApplicationDbContext>(
        //    (sp, option) => option
        //    .UseSqlServer(connectionString)
        //    .AddInterceptors(sp.GetRequiredService<SoftDeletedInterceptor>())
        //    .AddInterceptors(sp.GetRequiredService<CreatedInterceptor>())
        //    .AddInterceptors(sp.GetRequiredService<ModifiedInterceptor>())
        //    );

        services.AddDbContextFactory<ApplicationDbContext>(options =>
           options.UseSqlServer(connectionString));

        #region Repositories
        services.AddScoped<IFileItemRepository, FileItemRepository>();
        #endregion

        #region Files Repositories
        services.AddScoped<IBlogPostAttachmentRepository, BlogPostAttachmentRepository>();
        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProfileImageRepository, ProfileImageRepository>();
        #endregion




        return services;
    }
}