using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Exceptions;
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
            if (productoDTO == null || string.IsNullOrWhiteSpace(productoDTO.Nombre) || productoDTO.Precio <=0)
            {
                throw new BadRequestException("Datos de producto inválidos. Asegúrese de proporcionar un nombre y un precio válido.");
            }

            var productoExistente = await _context.productos
                .FirstOrDefaultAsync(p => p.Nombre == productoDTO.Nombre);

            if (productoExistente != null)
            {
                throw new ConflictException($"El producto '{productoDTO.Nombre}' ya existe.");
            }

            var nuevoProducto = new Producto
            {
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio
            };

            _context.productos.Add(nuevoProducto);
            await _context.SaveChangesAsync();

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

            if (!listarProductos.Any())
            {
                throw new NotFoundException("No hay productos disponibles.");
            }

            return listarProductos;
        }

    }

}
