using Prueba_Tecnica.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prueba_Tecnica.DTO
{
    public class IngresoOrdenDTO
    {
        [Required(ErrorMessage = "El cliente es obligatorio")]
        [StringLength(50,ErrorMessage = "el nombre no puede superar los 50 caracteres")]
        public required string Cliente { get; set; }
        [MinLength(1, ErrorMessage = "Se debe incluir al menos un producto")]
        public required List<int> listaProductosId { get; set; }
        
        [JsonIgnore]
        public double Total { get; set; }
    }
}