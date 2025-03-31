using Prueba_Tecnica.DTO;

namespace Prueba_Tecnica.Interfaces
{
    public interface IProductoInterface
    {
        Task<IngresoProductoDTO> CrearNuevoProducto(IngresoProductoDTO productoDTO);
        IEnumerable<IngresoProductoDTO> ListarTodosProductos();
    }
}
