using AspNetCoreSwaggerUse.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreSwaggerUse.Data
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}