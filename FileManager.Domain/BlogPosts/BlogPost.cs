using FileManager.Domain.Files.BlogPostFiles;

namespace FileManager.Domain.BlogPosts;
public class BlogPost
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public BlogPostAttachment? Attachment { get; set; }
}