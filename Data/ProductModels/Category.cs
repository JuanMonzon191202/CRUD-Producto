using System.ComponentModel.DataAnnotations;

namespace Productos.Data.ProductModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio.")]
        [MaxLength(
            100,
            ErrorMessage = "El nombre de la categoria no puede tener más de 100 caracteres."
        )]
        public string Name { get; set; }
    }
}
