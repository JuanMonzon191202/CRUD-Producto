using System.ComponentModel.DataAnnotations;

namespace Productos.Data.ProductModels
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [MaxLength(
            100,
            ErrorMessage = "El nombre del producto no puede tener más de 100 caracteres."
        )]
        public string Name { get; set; }

        [MaxLength(
            500,
            ErrorMessage = "La descripción del producto no puede tener más de 500 caracteres."
        )]
        public string Description { get; set; }

        [Required(ErrorMessage = "El precio del producto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "La categoría del producto es obligatoria.")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
