using FileManager.Domain.ProfileImages;
using FileManager.Domain.Services.Infrastructure.Storage;
using FileManager.Domain.Services.Infrastructure.Storage.Models;

namespace FileManager.Application.ProfileImageServices;
public class ProfileImageService : IProfileImageService
{
    private readonly IProfileImageRepository profileImageRepository;
    private readonly IDatabaseFileStorageService databaseFileStorageService;
    private readonly IFileTypeService fileTypeService;
    private static readonly string[] AllowedImageTypes =
    [
        "image/jpeg",
        "image/png"
    ];

    public ProfileImageService(
        IProfileImageRepository profileImageRepository,
        IDatabaseFileStorageService databaseFileStorageService,
        IFileTypeService fileTypeService)
    {
        this.profileImageRepository = profileImageRepository;
        this.databaseFileStorageService = databaseFileStorageService;
        this.fileTypeService = fileTypeService;
    }

    public async Task<List<ProfileImage>> GetProfileImagesForUserAsync(string userName, CancellationToken cancellationToken)
    {
        var profileImages = await profileImageRepository.GetAllAsync(cancellationToken);
        var userProfileImage = profileImages.Where(x => x.UserName == userName).ToList();

        return userProfileImage;
    }

    public async Task<Guid> UploadAsync(
        Stream fileStream,
        string fileName,
        string userName,
        CancellationToken cancellationToken)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            var fileType = fileTypeService.GetContentType(memoryStream);
            if (!AllowedImageTypes.Contains(fileType))
                throw new InvalidOperationException("❌ فقط فایل‌های JPEG و PNG مجاز هستند.");

            // Reset position for upload
            memoryStream.Position = 0;

            var uploadedFile = await databaseFileStorageService.UploadAsync(new FileUploadRequest
            {
                Name = fileName,
                ContentType = fileType,
                Content = memoryStream
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