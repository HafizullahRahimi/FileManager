using FileManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<FileItem> FileItems => Set<FileItem>();
}