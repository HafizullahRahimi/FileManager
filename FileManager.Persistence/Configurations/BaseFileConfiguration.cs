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
            .HasDiscriminator<FileStorageType>(f => f.StorageType)
            .HasValue<LocalFile>(FileStorageType.Local)
            .HasValue<DatabaseFile>(FileStorageType.Database)
            .HasValue<CloudFile>(FileStorageType.Cloud)
            .IsComplete();
    }
}