using FileManager.Domain.ProfileImages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Persistence.Configurations;
internal class ProfileImageConfiguration : IEntityTypeConfiguration<ProfileImage>
{
    public void Configure(EntityTypeBuilder<ProfileImage> builder)
    {

        // ProfileImage ↔ LocalFile: One-to-One
        builder
            .HasOne(img => img.DatabaseFile)
            .WithOne(f => f.ProfileImage)
            .HasForeignKey<ProfileImage>(img => img.DatabaseFileId)
            .OnDelete(DeleteBehavior.Cascade); // when a ProfileImage is deleted, the associated LocalFile is also deleted
    }
}