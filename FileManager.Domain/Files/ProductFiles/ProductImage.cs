using FileManager.Domain.Files.Base;
using FileManager.Domain.Products;

namespace FileManager.Domain.Files.ProductFiles;
public class ProductImage : LocalFile
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    // Optional: برای مشخص‌کردن نوع تصویر (اصلی، گالری، بندانگشتی و...)
    public ProductImageType ImageType { get; set; } = ProductImageType.Gallery;
}