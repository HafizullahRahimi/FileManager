using FileManager.Domain.ProfileImages;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories;
public class ProfileImageRepository : Repository<ProfileImage>, IProfileImageRepository
{
    public ProfileImageRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}