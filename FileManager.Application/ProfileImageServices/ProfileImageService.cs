using FileManager.Application.ProfileImageServices.Models;
using FileManager.Domain.Files;
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

    public async Task<ProfileImageDto?> GetImageByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var img = await profileImageRepository.GetByUserNameAsync(userName, cancellationToken);
        if (img?.DatabaseFile == null)
            return null;

        return new ProfileImageDto
        {
            Name = img.DatabaseFile.Name,
            Url = GetImageDataUrl(img.DatabaseFile)
        };
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

            var contentType = fileTypeService.GetContentType(memoryStream);
            if (!AllowedImageTypes.Contains(contentType))
                throw new InvalidOperationException("❌ فقط فایل‌های JPEG و PNG مجاز هستند.");

            // Reset position for upload
            memoryStream.Position = 0;

            var uploadedFile = await databaseFileStorageService.UploadAsync(new FileUploadRequest
            {
                FileName = fileName,
                ContentType = contentType,
                FileStream = memoryStream
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

    private static string GetImageDataUrl(DatabaseFile databaseFile)
    {
        if (databaseFile is null || databaseFile.Data is null)
            return string.Empty;

        var mimeType = databaseFile.ContentType?.ToLowerInvariant() ?? "application/octet-stream";
        var base64 = Convert.ToBase64String(databaseFile.Data);

        return $"data:{mimeType};base64,{base64}";
    }
}