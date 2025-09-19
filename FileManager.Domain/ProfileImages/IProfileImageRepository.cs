using FileManager.Domain.Base.Repositories;

namespace FileManager.Domain.ProfileImages;
public interface IProfileImageRepository : IRepository<ProfileImage>
{
    Task<ProfileImage?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
}