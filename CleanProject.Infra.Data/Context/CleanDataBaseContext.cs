using CleanProject.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CleanProject.Infra.Data.Context
{
    public class CleanDataBaseContext : DbContext
    {
        public CleanDataBaseContext(DbContextOptions<CleanDataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasQueryFilter(a => !a.IsDeleted);
        }
    }
}
