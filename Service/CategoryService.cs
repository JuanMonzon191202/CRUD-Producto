using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Data;
using Productos.Data.ProductModels;

namespace Productos.Services
{
    public class CategoryService
    {
        private readonly ProductContext _context;

        public CategoryService(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> CreateCategory(Category newCategory)
        {
            _context.Categorias.Add(newCategory);
            await _context.SaveChangesAsync();

            return newCategory;
        }

        public async Task<bool> UpdateCategory(int id, Category updatedCategory)
        {
            var existingCategory = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return false; 
            }

            existingCategory.Name = updatedCategory.Name;

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

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryToDelete = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoryToDelete == null)
            {
                return false; 
            }

            _context.Categorias.Remove(categoryToDelete);

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
