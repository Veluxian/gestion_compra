using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica.DTO
{
    public class IngresoNuevaOrden
    {
        [Required(ErrorMessage = "El cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "el nombre no puede superar los 50 caracteres")]
        public required string Cliente { get; set; }
        [MinLength(1, ErrorMessage = "Se debe incluir al menos un producto")]
        public required List<IngresoProductoDTO> listaProductos { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser positivo")]
        public double Total { get; set; }
    }
}
