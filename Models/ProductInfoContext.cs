using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProductApi.Models
{
    public partial class ProductInfoContext : DbContext
    {
        public ProductInfoContext()
        {
        }

        public ProductInfoContext(DbContextOptions<ProductInfoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Merchandise> Merchandises { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;
        public virtual DbSet<Test> Tests { get; set; } = null!;
        public virtual DbSet<Tire> Tires { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SOUL-DESKTOP\\MSSQL;Database=ProductInfo;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchandise>(entity =>
            {
                entity.ToTable("Merchandise");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.Area).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasMaxLength(50);

                entity.Property(e => e.Date).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Pay).HasMaxLength(10);

                entity.Property(e => e.Price).HasMaxLength(10);

                entity.Property(e => e.Quantity).HasMaxLength(10);

                entity.Property(e => e.Service).HasMaxLength(50);

                entity.Property(e => e.Spec).HasMaxLength(50);
            });

            modelBuilder.Entity<Test>(entity =>
            {
                entity.ToTable("Test");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            modelBuilder.Entity<Tire>(entity =>
            {
                entity.ToTable("Tire");

                entity.Property(e => e.Id).HasColumnName("ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
