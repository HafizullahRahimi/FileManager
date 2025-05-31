using FileManager.Domain.BlogPosts;
using FileManager.Domain.Entities;
using FileManager.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<FileItem> FileItems => Set<FileItem>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}