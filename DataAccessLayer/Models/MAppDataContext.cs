using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccessLayer.Models
{
    public partial class MAppDataContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=.;Database=MApplicationDb;user id=DESKTOP-EDIKHFL\\\\Tahsin ERKAVUN;password=;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryCode).HasMaxLength(100);

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ParentCategoryId).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserId)
                    .IsRequired()
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.CreateUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdateUserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");
            });
        }
    }
}
