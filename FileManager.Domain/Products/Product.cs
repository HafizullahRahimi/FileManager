using FileManager.Domain.Base;
using FileManager.Domain.Files.ProductFiles;

namespace FileManager.Domain.Products;
public class Product : FullAuditedEntity<Guid>
{
    public string Name { get; set; } = string.Empty;

    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>(); // required
}