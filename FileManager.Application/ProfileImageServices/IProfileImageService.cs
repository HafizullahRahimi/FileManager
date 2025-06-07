using FileManager.Domain.ProfileImages;
using FileManager.Domain.Services.Infrastructure.Storage.Models;

namespace FileManager.Application.ProfileImageServices;
public interface IProfileImageService
{
    Task<List<ProfileImage>> GetProfileImagesForUserAsync(string userName, CancellationToken cancellationToken);
    Task<Guid> UploadAsync(Stream fileStream, string fileName, string userName, CancellationToken cancellationToken);
    Task<FileData?> DownloadAsync(Guid id, CancellationToken cancellationToken);
}