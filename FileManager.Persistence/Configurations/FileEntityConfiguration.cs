using FileManager.Domain.Files.Base;
using FileManager.Domain.Files.BlogPostFiles;
using FileManager.Domain.Files.ProductFiles;
using FileManager.Domain.Files.UserFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Persistence.Configurations;
internal class FileEntityConfiguration : IEntityTypeConfiguration<BaseFile>
{
    public void Configure(EntityTypeBuilder<BaseFile> builder)
    {
        builder
            .ToTable("Files")
            .HasDiscriminator<string>("FileType")
            .HasValue<ProductImage>("ProductImage")
            .HasValue<BlogPostAttachment>("BlogPostAttachment")
            .HasValue<ProfileImage>("ProfileImage");
    }
}