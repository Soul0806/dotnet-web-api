using ProductApi.model;

using Microsoft.EntityFrameworkCore;
//using ProductApi.Models;

namespace ProductApi.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        //public DbSet<Product> Products { get; set; }
        public DbSet<Merchandise> Merchandise { get; set; }

        public DbSet<Tire> Tire { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Sale> Sale { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
