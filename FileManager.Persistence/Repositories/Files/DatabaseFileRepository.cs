using FileManager.Domain.Files;
using FileManager.Domain.Files.Repositories;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Files;
public class DatabaseFileRepository : Repository<DatabaseFile>, IDatabaseFileRepository
{
    public DatabaseFileRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}