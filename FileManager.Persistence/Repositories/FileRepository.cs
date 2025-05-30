using FileManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FileItem> GetByIdAndUserAsync(Guid id, string username)
        {
            return await _dbContext.FileItems
                .FirstOrDefaultAsync(f => f.Id == id && f.UploadedBy == username);
        }

        public async Task<List<FileItem>> GetAllByUserAsync(string username)
        {
            return await _dbContext.FileItems
                .Where(f => f.UploadedBy == username)
                .OrderByDescending(f => f.UploadedAt)
                .ToListAsync();
        }

        public async Task<FileItem> AddAsync(FileItem file)
        {
            _dbContext.FileItems.Add(file);
            await _dbContext.SaveChangesAsync();
            return file;
        }

        public async Task<bool> DeleteAsync(Guid id, string username)
        {
            var file = await GetByIdAndUserAsync(id, username);
            if (file == null) return false;

            _dbContext.FileItems.Remove(file);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}