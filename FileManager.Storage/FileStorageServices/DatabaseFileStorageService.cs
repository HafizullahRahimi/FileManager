using FileManager.Domain.Files;
using FileManager.Domain.Files.Repositories;
using FileManager.Domain.Services.Infrastructure.Storage;

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
        var file = new DatabaseFile
        {
            Name = request.Name,
            ContentType = GetContentType(request.Name),
            SizeInBytes = GetSizeInBytes(request.Content),
            Data = StreamToByteArray(request.Content)
        };

        return await repository.CreateAsync(file, cancellationToken);
    }
    public async Task<DatabaseFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await repository.GetByIdAsync(id, cancellationToken);
    public async Task DeleteAsync(DatabaseFile file, CancellationToken cancellationToken) =>
        await repository.DeleteAsync(file, cancellationToken);


    public async Task<byte[]> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await GetByIdAsync(id, cancellationToken)
                   ?? throw new FileNotFoundException("File not found in database.");

        return file.Data;
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

    private long GetSizeInBytes(Stream fileStream)
    {
        fileStream.Seek(0, SeekOrigin.Begin);
        using (var memoryStream = new MemoryStream())
        {
            fileStream.CopyTo(memoryStream);
            return memoryStream.Length;
        }
    }

    private static byte[] StreamToByteArray(Stream fileStream)
    {
        fileStream.Seek(0, SeekOrigin.Begin);
        using (var memoryStream = new MemoryStream())
        {
            fileStream.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
