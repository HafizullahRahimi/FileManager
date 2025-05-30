using FileManager.Application.FileServices.Modes;

namespace FileManager.Application.FileServices
{
    public interface IFileService
    {
        Task<List<FileDto>> GetFilesForUserAsync(string username);
        Task<Guid> UploadAsync(Stream fileStream, string fileName, string username);
        Task<(byte[] Content, string FileName)> DownloadAsync(Guid id, string username);
    }
}