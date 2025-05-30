using FileManager.Application.FileServices.Modes;
using FileManager.Domain.Entities;
using FileManager.Domain.Interfaces;

namespace FileManager.Application.FileServices;
public class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;
    private readonly IFileStorageService _storageService;

    public FileService(IFileRepository fileRepository, IFileStorageService storageService)
    {
        _fileRepository = fileRepository;
        _storageService = storageService;
    }

    private bool IsImageFile(string fileName)
    {
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
        return imageExtensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Guid> UploadAsync(Stream fileStream, string fileName, string username)
    {
        try
        {
            var storedPath = await _storageService.UploadAsync(fileStream, fileName);
            var file = new FileItem
            {
                Id = Guid.NewGuid(),
                FileName = fileName,
                StoredPath = storedPath,
                UploadedAt = DateTime.UtcNow,
                UploadedBy = username
            };
            await _fileRepository.AddAsync(file);
            return file.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<FileDto>> GetFilesForUserAsync(string username)
    {
        var files = await _fileRepository.GetAllByUserAsync(username);
        var result = new List<FileDto>();

        foreach (var file in files)
        {
            var isImage = IsImageFile(file.FileName);
            var dto = new FileDto
            {
                Id = file.Id,
                FileName = file.FileName,
                UploadedAt = file.UploadedAt,
                IsImage = isImage
            };

            if (isImage)
            {
                try
                {
                    dto.ImageContent = await _storageService.DownloadAsync(file.StoredPath);
                }
                catch (Exception)
                {
                    // If we can't load the image content, we'll still return the file info
                    // but without the image content
                    dto.IsImage = false;
                }
            }

            result.Add(dto);
        }

        return result;
    }

    public async Task<(byte[] Content, string FileName)> DownloadAsync(Guid id, string username)
    {
        var file = await _fileRepository.GetByIdAndUserAsync(id, username);
        if (file == null) throw new Exception("Unauthorized or file not found.");
        var content = await _storageService.DownloadAsync(file.StoredPath);
        return (content, file.FileName);
    }
}