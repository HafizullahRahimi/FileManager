namespace FileManager.Domain.Entities
{
    public interface IFileItemRepository
    {
        Task<FileItem> GetByIdAndUserAsync(Guid id, string username);
        Task<List<FileItem>> GetAllByUserAsync(string username);
        Task<FileItem> AddAsync(FileItem file);
        Task<bool> DeleteAsync(Guid id, string username);
    }
}