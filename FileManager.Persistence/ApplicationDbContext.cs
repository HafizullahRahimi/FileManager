using FileManager.Domain.BlogPostAttachments;
using FileManager.Domain.BlogPosts;
using FileManager.Domain.Entities;
using FileManager.Domain.Files.ProductFiles;
using FileManager.Domain.Products;
using FileManager.Domain.ProfileImages;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<FileItem> FileItems => Set<FileItem>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    public DbSet<BlogPostAttachment> BlogPostAttachments => Set<BlogPostAttachment>();
    public DbSet<ProfileImage> ProfileImages => Set<ProfileImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}