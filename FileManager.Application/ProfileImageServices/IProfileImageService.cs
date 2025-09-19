using FileManager.Application.ProfileImageServices.Models;

namespace FileManager.Application.ProfileImageServices;
public interface IProfileImageService
{
    Task<ProfileImageDto?> GetImageByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<Guid> UploadAsync(Stream fileStream, string fileName, string userName, CancellationToken cancellationToken);
}