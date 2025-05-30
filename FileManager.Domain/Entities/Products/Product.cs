using FileManager.Domain.Entities.Files.ProductFiles;

namespace FileManager.Domain.Entities.Products;
public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
}