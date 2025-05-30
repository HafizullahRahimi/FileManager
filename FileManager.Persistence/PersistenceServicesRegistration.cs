using FileManager.Domain.Entities;
using FileManager.Persistence.Repositories;
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

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

        #region Repositories
        services.AddScoped<IFileItemRepository, FileItemRepository>();
        #endregion




        return services;
    }
}