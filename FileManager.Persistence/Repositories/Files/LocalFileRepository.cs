using FileManager.Domain.Files;
using FileManager.Domain.Files.Repositories;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Files;
public class LocalFileRepository : Repository<LocalFile>, ILocalFileRepository
{
    public LocalFileRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}