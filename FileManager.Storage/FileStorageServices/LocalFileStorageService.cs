using FileManager.Domain.Files;
using FileManager.Domain.Files.Repositories;
using FileManager.Domain.Services.Infrastructure.Storage;
using Microsoft.Extensions.Hosting;

namespace FileManager.Storage.FileStorageServices;
public class LocalFileStorageService : ILocalFileStorageService
{
    private ILocalFileRepository repository;
    private readonly string storageRootPath;
    private const string uploadsDir = "Uploads";
    private const string tempDir = "Temp";
    private const string archiveDir = "Archive";

    public LocalFileStorageService(ILocalFileRepository repository, IHostEnvironment env)
    {
        this.repository = repository;
        storageRootPath = Path.Combine(env.ContentRootPath, "Storage");
        EnsureDirectoryStructure();
    }
    public async Task<LocalFile> UploadAsync(FileUploadRequest request, CancellationToken cancellationToken)
    {
        string fileType = DetermineFileType(request.Name);
        string targetDir = Path.Combine(storageRootPath, uploadsDir, fileType);

        string fileName = GenerateUniqueFileName(request.Name);
        var filePath = Path.Combine(targetDir, fileName);

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await request.Content.CopyToAsync(fileStream, cancellationToken);

        var localFile = new LocalFile
        {
            Name = request.Name,
            ContentType = request.ContentType,
            SizeInBytes = request.Content.Length,
            Path = filePath,
            StorageType = FileStorageType.Local
        };

        await repository.CreateAsync(localFile, CancellationToken.None);

        return localFile;
    }

    public Task<LocalFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task DeleteAsync(LocalFile file, CancellationToken cancellationToken)
    {
        if (File.Exists(file.Path))
        {
            //File.Delete(file.Path);
            await DeleteContentAsync(file.Path);
        }
        await repository.DeleteAsync(file, cancellationToken);
    }


    public async Task<byte[]> DownloadAsync(string path, CancellationToken cancellationToken)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("The requested file was not found.", path);

        return await File.ReadAllBytesAsync(path, cancellationToken);
    }

    private void EnsureDirectoryStructure()
    {
        var directories = new[]
        {
                storageRootPath,
                Path.Combine(storageRootPath, uploadsDir),
                Path.Combine(storageRootPath, tempDir),
                Path.Combine(storageRootPath, archiveDir),
                // Add subdirectories for different file types
                Path.Combine(storageRootPath, uploadsDir, "Documents"),
                Path.Combine(storageRootPath, uploadsDir, "Images"),
                Path.Combine(storageRootPath, uploadsDir, "Media")
            };

        foreach (var dir in directories)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
    }

    private string DetermineFileType(string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLowerInvariant();
        return extension switch
        {
            ".pdf" or ".doc" or ".docx" or ".txt" => "documents",
            ".jpg" or ".jpeg" or ".png" or ".gif" => "images",
            ".mp4" or ".mp3" or ".wav" or ".avi" => "media",
            _ => "other"
        };
    }

    private string GenerateUniqueFileName(string originalFileName)
    {
        string extension = Path.GetExtension(originalFileName);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
        return $"{fileNameWithoutExt}_{DateTime.UtcNow:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{extension}";
    }

    private Task<bool> DeleteContentAsync(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                string fileName = Path.GetFileName(path);
                string archivePath = Path.Combine(storageRootPath, archiveDir, fileName);
                File.Move(path, archivePath, true);
            }
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}