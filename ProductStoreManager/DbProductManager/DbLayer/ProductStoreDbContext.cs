using System.Data.Entity;
using ServiceDataContracts;
using StoreManager.Common;

namespace StoreManager.DbProductManager.DbLayer
{
    public class ProductStoreDbContext : DbContext
    {
        public DbSet<Category> CategoryItems { get; set; }
        public DbSet<Sort> SortItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>()
                .HasKey(c => c.Id);
            modelBuilder
               .Entity<Category>()
               .Property(c => c.CreatedOn).HasColumnType("datetime2");
            modelBuilder
               .Entity<Category>()
               .Property(c => c.ModifiedOn).HasColumnType("datetime2");
            modelBuilder
                .Entity<Category>()
                .ToTable("Category");
            modelBuilder
                .Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasUniqueIndexAnnotation("Unique_CategoryName", 0);


            modelBuilder
               .Entity<Sort>()
               .HasKey(s => s.Id);
            modelBuilder
              .Entity<Sort>()
              .Property(s => s.CreatedOn).HasColumnType("datetime2");
            modelBuilder
              .Entity<Sort>()
              .Property(s => s.ModifiedOn).HasColumnType("datetime2");
            modelBuilder
                .Entity<Sort>()
                .ToTable("Sort");
            modelBuilder
                .Entity<Sort>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasUniqueIndexAnnotation("Unique_SortName", 0);


            modelBuilder
              .Entity<Product>()
              .ToTable("Product");
            modelBuilder
              .Entity<Product>()
              .Property(p => p.PublishedDate).HasColumnType("datetime2");
            modelBuilder
              .Entity<Product>()
              .Property(p => p.UploadDate).HasColumnType("datetime2");

            //modelBuilder.Entity<Product>()
            //  .HasRequired(p => p.Category)
            //  .WithMany(c => c.Products)
            //  .Map(m => m.MapKey("CategoryId"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
