using FileManager.Domain.Files;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Persistence.Configurations;
internal class BaseFileConfiguration : IEntityTypeConfiguration<BaseFile>
{
    public void Configure(EntityTypeBuilder<BaseFile> builder)
    {
        builder
            .ToTable("Files")
            .HasDiscriminator<string>("FileType")
            .HasValue<LocalFile>("LocalFile")
            .HasValue<DatabaseFile>("DatabaseFile")
            .HasValue<CloudFile>("CloudFile");
    }
}