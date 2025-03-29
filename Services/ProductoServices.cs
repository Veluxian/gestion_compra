using Prueba_Tecnica.Data;
using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;

namespace Prueba_Tecnica.Services
{
    public class ProductoServices : IProductoInterface
    {
        private readonly PruebaTecnicaDbContext _context;
        
        public ProductoServices(PruebaTecnicaDbContext context)
        {
            _context = context;
        }

        public Task<IngresoProductoDTO> CrearNuevoProducto(IngresoProductoDTO productoDTO)
        {
            return null;
        }
        public IEnumerable<IngresoProductoDTO> ListarTodosProductos()
        {
            var listarProductos = _context.productos
                .Select(pc => new IngresoProductoDTO
                {
                    Nombre = pc.Nombre,
                    Precio = pc.Precio
                }).ToList();
            return listarProductos;
        }

    }

}
