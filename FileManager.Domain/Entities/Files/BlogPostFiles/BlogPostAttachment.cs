using FileManager.Domain.Entities.BlogPosts;

namespace FileManager.Domain.Entities.Files.BlogPostFiles;
public class BlogPostAttachment : FileEntity
{
    public Guid BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }

    // Optional: برای تعیین نوع فایل ضمیمه (مثلاً PDF, Word, Image, etc.)
    public string? Description { get; set; }
}