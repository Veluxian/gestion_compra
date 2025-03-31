using System.ComponentModel.DataAnnotations;
namespace Prueba_Tecnica.DTO
{
    public class IngresoProductoDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre debe ser menor a 50 caracteres")]
        public required string Nombre { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public required double Precio { get; set; }
    }
}