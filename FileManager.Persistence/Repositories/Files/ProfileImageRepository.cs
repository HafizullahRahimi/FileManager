using FileManager.Domain.Files.ProductFiles;
using FileManager.Domain.Files.UserFiles;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Files;
public class ProfileImageRepository : Repository<ProfileImage>, IProfileImageRepository
{
    public ProfileImageRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}