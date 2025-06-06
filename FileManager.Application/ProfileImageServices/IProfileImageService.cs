using FileManager.Domain.ProfileImages;

namespace FileManager.Application.ProfileImageServices;
public interface IProfileImageService
{
    Task<List<ProfileImage>> GetProfileImagesForUserAsync(string username, CancellationToken cancellationToken);
    Task<Guid> UploadAsync(Stream fileStream, string fileName, string username, CancellationToken cancellationToken);
}