using FileManager.Domain.Files;
using FileManager.Domain.Files.Repositories;
using FileManager.Domain.Services.Infrastructure.Storage;
using FileManager.Domain.Services.Infrastructure.Storage.Models;

namespace FileManager.Storage.FileStorageServices;
public class DatabaseFileStorageService : IDatabaseFileStorageService
{
    private readonly IDatabaseFileRepository repository;

    public DatabaseFileStorageService(IDatabaseFileRepository repository)
    {
        this.repository = repository;
    }
    public async Task<DatabaseFile> UploadAsync(FileUploadRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var (data, size) = await ReadStreamOnceAsync(request.Content, cancellationToken);

            var file = new DatabaseFile
            {
                Name = request.Name,
                ContentType = GetContentType(request.Name),
                SizeInBytes = size,
                Data = data
            };

            return await repository.CreateAsync(file, cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<DatabaseFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken);
    public async Task DeleteAsync(DatabaseFile file, CancellationToken cancellationToken) =>
        await repository.DeleteAsync(file, cancellationToken);

    public async Task<FileData?> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await GetByIdAsync(id, cancellationToken);
        if (file != null)
            return new FileData(file.Data, file.Name);

        return null;
        throw new FileNotFoundException($"File with ID {id} not found.");
    }

    private string GetContentType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".pdf" => "application/pdf",
            ".doc" or ".docx" => "application/msword",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".mp4" => "video/mp4",
            ".mp3" => "audio/mpeg",
            _ => "application/octet-stream"
        };
    }

    private static async Task<(byte[] Data, long Size)> ReadStreamOnceAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, cancellationToken);
        return (memoryStream.ToArray(), memoryStream.Length);
    }
}
