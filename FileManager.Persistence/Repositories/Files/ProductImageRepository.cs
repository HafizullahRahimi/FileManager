using FileManager.Domain.Files.ProductFiles;
using FileManager.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Persistence.Repositories.Files;
public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
{
    protected ProductImageRepository(IDbContextFactory<ApplicationDbContext> dbcontextFactory) : base(dbcontextFactory)
    {
    }
}