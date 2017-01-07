using System.Data.Entity;
using ServiceDataContracts;
using StoreManager.Common;

namespace StoreManager.FoxEBookProductManager.DbLayer
{
    public class FoxEbookDbContext : DbContext
    {
        public DbSet<FoxeCategory> CategoryItems { get; set; }
        public DbSet<FoxeSort> SortItems { get; set; }
        public DbSet<FoxeProduct> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<FoxeCategory>()
                .HasKey(c => c.Id);
            modelBuilder
               .Entity<FoxeCategory>()
               .Property(c => c.CreatedOn).HasColumnType("datetime2");
            modelBuilder
               .Entity<FoxeCategory>()
               .Property(c => c.ModifiedOn).HasColumnType("datetime2");
            modelBuilder
                .Entity<FoxeCategory>()
                .ToTable("Category");
            modelBuilder
                .Entity<FoxeCategory>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasUniqueIndexAnnotation("Unique_CategoryName", 0);
            modelBuilder
                .Entity<FoxeCategory>()
                .HasOptional(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(pc => pc.ParentCategoryId);

            modelBuilder
               .Entity<FoxeSort>()
               .HasKey(s => s.Id);
            modelBuilder
              .Entity<FoxeSort>()
              .Property(s => s.CreatedOn).HasColumnType("datetime2");
            modelBuilder
              .Entity<FoxeSort>()
              .Property(s => s.ModifiedOn).HasColumnType("datetime2");
            modelBuilder
                .Entity<FoxeSort>()
                .ToTable("Sort");
            modelBuilder
                .Entity<FoxeSort>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasUniqueIndexAnnotation("Unique_SortName", 0);


            modelBuilder
              .Entity<FoxeProduct>()
              .ToTable("Product");
            modelBuilder
              .Entity<FoxeProduct>()
              .Property(p => p.PublishedDate).HasColumnType("datetime2");
            modelBuilder
              .Entity<FoxeProduct>()
              .Property(p => p.UploadDate).HasColumnType("datetime2");
            modelBuilder
              .Entity<FoxeProduct>()
              .HasRequired(p => p.Page)
              .WithMany(p => p.Products)
              .WillCascadeOnDelete(false);

            modelBuilder
              .Entity<FoxePage>()
              .ToTable("Page");
            modelBuilder
              .Entity<FoxePage>()
              .Property(p => p.CreatedOn).HasColumnType("datetime2");
            modelBuilder
              .Entity<FoxePage>()
              .Property(p => p.ModifiedOn).HasColumnType("datetime2");
            modelBuilder
             .Entity<FoxePage>()
             .Ignore(p => p.DownloadedProductsCount);

            base.OnModelCreating(modelBuilder);
        }
    }
}
