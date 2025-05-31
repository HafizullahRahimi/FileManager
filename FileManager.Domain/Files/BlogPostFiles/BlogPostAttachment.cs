using FileManager.Domain.BlogPosts;
using FileManager.Domain.Files.Base;

namespace FileManager.Domain.Files.BlogPostFiles;
public class BlogPostAttachment : LocalFile
{
    public Guid BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; } = new BlogPost();

    // Optional: برای تعیین نوع فایل ضمیمه (مثلاً PDF, Word, Image, etc.)
    public string? Description { get; set; }
}