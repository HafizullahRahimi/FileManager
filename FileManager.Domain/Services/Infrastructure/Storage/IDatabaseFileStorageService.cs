using FileManager.Domain.Files;
using FileManager.Domain.Services.Infrastructure.Storage.Models;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface IDatabaseFileStorageService : IFileStorageService<DatabaseFile>
{
    Task<FileData?> DownloadAsync(Guid id, CancellationToken cancellationToken);
}