using Microsoft.EntityFrameworkCore;
using Productos.Data.ProductModels;

namespace Productos.Data
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorias { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
