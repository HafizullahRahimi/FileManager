using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FileManager.Web.Data
{
    public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}
