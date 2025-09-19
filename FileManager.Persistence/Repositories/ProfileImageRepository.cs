using FileManager.Domain.Base;
using FileManager.Domain.Base.Repositories;
using FileManager.Domain.ProfileImages;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories;
public class ProfileImageRepository : Repository<ProfileImage>, IProfileImageRepository
{
    public ProfileImageRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }

    public async Task<ProfileImage?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var filter = new Filter<ProfileImage>(pi => pi.UserName == userName);
        return await GetAsync(filter, cancellationToken);
    }
}