namespace Prueba_Tecnica.Models
{
    public class OrdenCompra
    {
        public int IdOrden {  get; set; }
        public required string Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public double Total {  get; set; }

        public required ICollection<OrdenProducto> OrdenProductos { get; set; }
    }
}
