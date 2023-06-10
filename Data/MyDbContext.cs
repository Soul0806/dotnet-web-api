using ProductApi.model;

using Microsoft.EntityFrameworkCore;

namespace ProductApi.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        //public DbSet<Product> Products { get; set; }
        public DbSet<Merchandise> Merchandise { get; set; }
        public DbSet<Test> Test { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
