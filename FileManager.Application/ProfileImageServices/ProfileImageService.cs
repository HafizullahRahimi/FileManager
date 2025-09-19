using FileManager.Application.ProfileImageServices.Models;
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
        var img = await profileImageRepository.GetByUserNameAsync(userName,cancellationToken);
        if (img == null)
            return null;

        var imageDto = new ProfileImageDto
        {
            Id = img.Id,
            Name = img.DatabaseFile?.Name ?? string.Empty,
            Data = img.DatabaseFile?.Data ?? Array.Empty<byte>(),
            Url = GetImageDataUrl(img),
            CreatedUtcDate = img.CreatedUtcDate
        };
        return imageDto;
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

    private string GetImageDataUrl(ProfileImage profileImage)
    {
        if (profileImage?.DatabaseFile == null)
            throw new ArgumentNullException(nameof(profileImage));

        var mimeType = profileImage.DatabaseFile.ContentType.ToLowerInvariant();
        return $"data:{mimeType};base64,{Convert.ToBase64String(profileImage.DatabaseFile.Data)}";
    }
}