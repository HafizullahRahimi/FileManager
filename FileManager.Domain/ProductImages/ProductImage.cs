using FileManager.Domain.Base;
using FileManager.Domain.Products;

namespace FileManager.Domain.Files.ProductFiles;
public class ProductImage : FullAuditedEntity<Guid>
{
    public ProductImageType ImageType { get; set; } = ProductImageType.Gallery;

    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; } = null!; // required

    public Guid LocalFileId { get; set; }
    public virtual LocalFile LocalFile { get; set; } = null!; // required
}