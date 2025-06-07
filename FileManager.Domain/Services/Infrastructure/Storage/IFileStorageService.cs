using FileManager.Domain.Files;
using FileManager.Domain.Services.Infrastructure.Storage.Models;

namespace FileManager.Domain.Services.Infrastructure.Storage;
public interface IFileStorageService<TFile> where TFile : BaseFile
{
    Task<TFile> UploadAsync(FileUploadRequest request, CancellationToken cancellationToken);
    Task<TFile?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(TFile file, CancellationToken cancellationToken);
}