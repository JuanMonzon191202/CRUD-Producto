using Microsoft.AspNetCore.Mvc;
using Productos.Data.ProductModels;
using Productos.Services;

namespace Productos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoryService _service;

        public CategoriaController(CategoryService service)
        {
            _service = service;
        }

        [HttpGet("categorias")]
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _service.GetAllCategorias();
        }

        [HttpGet("categorias/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById(int id)
        {
            var category = await _service.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("categorias")]
        public async Task<ActionResult<Category>> CreateCategory(Category newCategory)
        {
            var createdCategory = await _service.CreateCategory(newCategory);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = createdCategory.Id },
                createdCategory
            );
        }

        [HttpPut("categorias/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            var success = await _service.UpdateCategory(id, updatedCategory);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("categorias/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var success = await _service.DeleteCategory(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
