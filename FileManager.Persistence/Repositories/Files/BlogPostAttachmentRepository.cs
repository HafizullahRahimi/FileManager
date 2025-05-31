using FileManager.Domain.Files.BlogPostFiles;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Files;
public class BlogPostAttachmentRepository : Repository<BlogPostAttachment>, IBlogPostAttachmentRepository
{
    protected BlogPostAttachmentRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}