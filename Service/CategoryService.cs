using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Productos.Data;
using Productos.Data.ProductModels;

namespace BackEdn.Services
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
                return false; // La categoría no existe, no se puede actualizar.
            }

            existingCategory.Name = updatedCategory.Name;

            try
            {
                await _context.SaveChangesAsync();
                return true; // Actualización exitosa.
            }
            catch (DbUpdateException)
            {
                // Manejar excepciones, si es necesario.
                return false; // Error al actualizar.
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryToDelete = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoryToDelete == null)
            {
                return false; // La categoría no existe, no se puede eliminar.
            }

            _context.Categorias.Remove(categoryToDelete);

            try
            {
                await _context.SaveChangesAsync();
                return true; // Eliminación exitosa.
            }
            catch (DbUpdateException)
            {
                // Manejar excepciones, si es necesario.
                return false; // Error al eliminar.
            }
        }
    }
}
