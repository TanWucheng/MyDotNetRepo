using System;
using MatBlazor.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace MatBlazorDemo.Data
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
