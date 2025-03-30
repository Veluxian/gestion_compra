using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Services
{
    public class ProductoServices : IProductoInterface
    {
        private readonly PruebaTecnicaDbContext _context;
        
        public ProductoServices(PruebaTecnicaDbContext context)
        {
            _context = context;
        }

        public async Task<IngresoProductoDTO> CrearNuevoProducto(IngresoProductoDTO productoDTO)
        {
            // 1. Validar si el producto ya existe por nombre
            var productoExistente = await _context.productos
                .FirstOrDefaultAsync(p => p.Nombre == productoDTO.Nombre);

            if (productoExistente != null)
            {
                // Opcional: Lanzar excepción o retornar null si prefieres
                return null; // Producto ya existe
            }

            // 2. Crear la entidad Producto desde el DTO
            var nuevoProducto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio
            };

            // 3. Guardar en la base de datos
            _context.productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();

            // 4. Retornar el mismo DTO (como en tu lógica de órdenes)
            return productoDTO;
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
