using FileManager.Application.ProfileImageServices.Models;
using FileManager.Domain.ProfileImages;

namespace FileManager.Application.ProfileImageServices;
public interface IProfileImageService
{
    Task<List<ProfileImageDto>> GetProfileImagesForUserAsync(string userName, CancellationToken cancellationToken);
    string GetImageDataUrl(ProfileImage profileImage);
    Task<Guid> UploadAsync(Stream fileStream, string fileName, string userName, CancellationToken cancellationToken);
    Task<(byte[] content, string fileName)> DownloadAsync(Guid profileImageId, CancellationToken cancellationToken);
}