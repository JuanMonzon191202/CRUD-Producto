using Microsoft.AspNetCore.Mvc;
using Productos.Data.ProductModels;
using Productos.Services;

namespace Productos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly ProductoService _service;

        public ProductoController(ProductoService service)
        {
            _service = service;
        }

        [HttpGet("productos")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _service.GetAllProducts();
        }

        [HttpGet("producto/{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _service.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("producto")]
        public async Task<ActionResult<Product>> CreateProduct(Product newProduct)
        {
            var createdProduct = await _service.CreateProducto(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("producto/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product updatedProduct)
        {
            var success = await _service.UpdateProduct(id, updatedProduct);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("producto/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _service.DeleteProduct(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
