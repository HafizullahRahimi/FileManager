using FileManager.Domain.ProfileImages;
using FileManager.Domain.Services.Infrastructure.Storage;
using FileManager.Domain.Services.Infrastructure.Storage.Models;

namespace FileManager.Application.ProfileImageServices;
public class ProfileImageService : IProfileImageService
{
    private readonly IProfileImageRepository profileImageRepository;
    private readonly IDatabaseFileStorageService databaseFileStorageService;

    public ProfileImageService(IProfileImageRepository profileImageRepository, IDatabaseFileStorageService databaseFileStorageService)
    {
        this.profileImageRepository = profileImageRepository;
        this.databaseFileStorageService = databaseFileStorageService;
    }

    public async Task<List<ProfileImage>> GetProfileImagesForUserAsync(string userName, CancellationToken cancellationToken)
    {
        var profileImages = await profileImageRepository.GetAllAsync(cancellationToken);
        var userProfileImage = profileImages.Where(x => x.UserName == userName).ToList();

        return userProfileImage;
    }

    public async Task<Guid> UploadAsync(Stream fileStream, string fileName, string userName, CancellationToken cancellationToken)
    {
        try
        {
            var uploadedFile = await databaseFileStorageService.UploadAsync(new FileUploadRequest
            {
                Name = fileName,
                ContentType = "image/jpeg",
                Content = fileStream
            }, cancellationToken);


            var profileImage = new ProfileImage
            {
                UserName = userName,
                DatabaseFileId = uploadedFile.Id
            };

            var newProfileImage = await profileImageRepository.CreateAsync(profileImage, cancellationToken);
            return newProfileImage.Id;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public async Task<FileData?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await databaseFileStorageService.DownloadAsync(id, cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }
}