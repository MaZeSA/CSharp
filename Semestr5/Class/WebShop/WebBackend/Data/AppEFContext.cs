using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebBackend.Data.Entities;

namespace WebBackend.Data
{
    public class AppEFContext : IdentityDbContext<AplicattionUser>
    {
        public AppEFContext(DbContextOptions<AppEFContext> options)
            :base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { set; get; }
        public DbSet<UserEntity> Users { set; get; }
    }
}
