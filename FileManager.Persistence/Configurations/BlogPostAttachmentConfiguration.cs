using FileManager.Domain.BlogPostAttachments;
using FileManager.Domain.BlogPosts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace FileManager.Persistence.Configurations;
internal class BlogPostAttachmentConfiguration : IEntityTypeConfiguration<BlogPostAttachment>
{
    public void Configure(EntityTypeBuilder<BlogPostAttachment> builder)
    {
        // BlogPost ↔ BlogPostAttachment: One-to-One
        builder
            .HasOne(a => a.BlogPost)
            .WithOne(b => b.Attachment)
            .HasForeignKey<BlogPostAttachment>(a => a.BlogPostId)
            .OnDelete(DeleteBehavior.Cascade); // when a BlogPost is deleted, the associated BlogPostAttachment is also deleted

        // BlogPostAttachment ↔ LocalFile: One-to-One
        builder
            .HasOne(a => a.LocalFile)
            .WithOne(f => f.BlogPostAttachment)
            .HasForeignKey<BlogPostAttachment>(a => a.LocalFileId)
            .OnDelete(DeleteBehavior.Cascade); // when a BlogPostAttachment is deleted, the associated LocalFile is also deleted
    }
}