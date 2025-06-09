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
            var bytes = await GetBytesAsync(request.FileStream, cancellationToken);

            var file = new DatabaseFile
            {
                Name = request.FileName,
                ContentType = request.ContentType,
                SizeInBytes = request.FileStream.Length,
                Data = bytes
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

    private static async Task<byte[]> GetBytesAsync(Stream stream, CancellationToken cancellationToken)
    {
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}
