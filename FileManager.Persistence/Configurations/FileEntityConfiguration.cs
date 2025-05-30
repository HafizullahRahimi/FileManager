using FileManager.Domain.Entities.Files;
using FileManager.Domain.Entities.Files.BlogPostFiles;
using FileManager.Domain.Entities.Files.ProductFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Persistence.Configurations;
internal class FileEntityConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder
            .ToTable("Files")
            .HasDiscriminator<string>("FileType")
            .HasValue<ProductImage>("ProductImage")
            .HasValue<BlogPostAttachment>("BlogPostAttachment");
    }
}