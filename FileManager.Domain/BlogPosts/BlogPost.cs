using FileManager.Domain.Base;
using FileManager.Domain.BlogPostAttachments;

namespace FileManager.Domain.BlogPosts;
public class BlogPost : FullAuditedEntity<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public BlogPostAttachment? Attachment { get; set; }
}