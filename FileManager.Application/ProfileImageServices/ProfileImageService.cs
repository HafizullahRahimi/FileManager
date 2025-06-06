using FileManager.Domain.ProfileImages;
using FileManager.Domain.Services.Infrastructure.Storage;

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
    public async Task<List<ProfileImage>> GetProfileImagesForUserAsync(string username, CancellationToken cancellationToken)
    {
        var profileImages = await profileImageRepository.GetAllAsync(cancellationToken);
        var userProfileImage = profileImages.Where(x => x.UserName == username).ToList();

        return userProfileImage;
    }

    public async Task<Guid> UploadAsync(Stream fileStream, string fileName, string username, CancellationToken cancellationToken)
    {
        var uploadedFile = await databaseFileStorageService.UploadAsync(new FileUploadRequest
        {
            Name = fileName,
            ContentType = "image/jpeg",
            Content = fileStream
        }, cancellationToken);


        var profileImage = new ProfileImage
        {
            UserName = username,
            DatabaseFileId = uploadedFile.Id
        };

        var newProfileImage = await profileImageRepository.CreateAsync(profileImage, cancellationToken);
        return newProfileImage.Id;

    }
}