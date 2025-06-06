using FileManager.Domain.Files;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface IFileStorageService<TFile> where TFile : BaseFile
{
    Task<TFile> UploadAsync(FileUploadRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(TFile file, CancellationToken cancellationToken);
    Task<TFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}