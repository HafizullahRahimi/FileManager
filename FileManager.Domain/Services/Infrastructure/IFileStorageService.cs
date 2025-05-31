namespace FileManager.Domain.Services.Infrastructure
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName);
        Task<byte[]> DownloadAsync(string storedPath);
        Task<bool> DeleteAsync(string storedPath);
    }
}