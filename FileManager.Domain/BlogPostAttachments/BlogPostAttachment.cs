using FileManager.Domain.Base;
using FileManager.Domain.BlogPosts;
using FileManager.Domain.Files;

namespace FileManager.Domain.BlogPostAttachments;
public class BlogPostAttachment : FullAuditedEntity<Guid>
{
    public string? Description { get; set; }

    public Guid BlogPostId { get; set; }
    public virtual BlogPost? BlogPost { get; set; }

    public Guid LocalFileId { get; set; }
    public virtual LocalFile LocalFile { get; set; } = null!; //required 
}