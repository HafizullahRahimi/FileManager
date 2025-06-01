using FileManager.Domain.BlogPostAttachments;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories;
public class BlogPostAttachmentRepository : Repository<BlogPostAttachment>, IBlogPostAttachmentRepository
{
    public BlogPostAttachmentRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}