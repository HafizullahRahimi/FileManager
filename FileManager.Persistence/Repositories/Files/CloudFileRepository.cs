using FileManager.Domain.Files;
using FileManager.Domain.Files.Repositories;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Files;
public class CloudFileRepository : Repository<CloudFile>, ICloudFileRepository
{
    public CloudFileRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}