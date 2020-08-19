using DbTableToDotnetEntity.Models;
using Microsoft.EntityFrameworkCore;

namespace DbTableToDotnetEntity.Data
{
    public class EfCoreContext : DbContext
    {
        public EfCoreContext(DbContextOptions<EfCoreContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }

        public DbSet<TableInfo> Tables { get; set; }

        public DbSet<ColumnInfo> Columns { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Users>().HasNoKey();
        }
    }
}
