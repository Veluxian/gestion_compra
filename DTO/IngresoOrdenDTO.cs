namespace Prueba_Tecnica.DTO
{
    public class IngresoOrdenDTO
    {
        public required string Cliente { get; set; }
        public required List<IngresoProductoDTO> listaProductos { get; set; }
        public required double Total { get; set; }
    }
}