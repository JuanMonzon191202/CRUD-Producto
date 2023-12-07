using Microsoft.EntityFrameworkCore;
using Productos.Data;
using Productos.Data.ProductModels;

namespace Productos.Services
{
    public class ProductoService
    {
        private readonly ProductContext _context;

        public ProductoService(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var productosget = await _context.Products.Include(p => p.Category).ToListAsync();

            return productosget;
        }

        public async Task<Product?> GetById(int id)
        {
            var productoFind = await _context
                .Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            return productoFind;
        }

        public async Task<Product> CreateProducto(Product newProduct)
        {
            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();

            return newProduct;
        }

        public async Task<bool> UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Stock = updatedProduct.Stock;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var productToDelete = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (productToDelete == null)
            {
                return false;
            }

            _context.Products.Remove(productToDelete);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
