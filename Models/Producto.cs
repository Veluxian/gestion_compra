namespace Prueba_Tecnica.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public required string Nombre { get; set; }
        public double Precio { get; set; }

        public required ICollection<OrdenProducto> OrdenProducto { get; set; }
    }
}
