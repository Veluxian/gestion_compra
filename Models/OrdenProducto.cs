namespace Prueba_Tecnica.Models
{
    public class OrdenProducto
    {
        public int IdOrdenProducto { get; set; }
        public int IdOrden {  get; set; }
        public int IdProducto { get; set; }

        public required OrdenCompra OrdenCompra { get; set; }
        public required Producto Producto { get; set; }
    }
}
