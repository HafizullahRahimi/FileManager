using FileManager.Domain.Files.ProductFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FileManager.Persistence.Configurations;
internal class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        // Product ↔ ProductImage: One-to-Many
        builder
            .HasOne(img => img.Product)
            .WithMany(p => p.ProductImages)
            .HasForeignKey(img => img.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // when a Product is deleted, all associated ProductImages are also deleted

        // ProductImage ↔ LocalFile: One-to-One
        builder
            .HasOne(img => img.LocalFile)
            .WithOne(f => f.ProductImage)
            .HasForeignKey<ProductImage>(img => img.LocalFileId)
            .OnDelete(DeleteBehavior.Cascade); // when a ProductImage is deleted, the associated LocalFile is also deleted
    }
}