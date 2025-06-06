using FileManager.Domain.Files;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface IDatabaseFileStorageService : IFileStorageService<DatabaseFile>
{
    Task<byte[]> DownloadAsync(Guid fileId, CancellationToken cancellationToken);
}