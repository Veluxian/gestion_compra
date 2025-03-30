using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Services
{
    public class OrdenesService : IOrdenesInterface
    {
        private readonly PruebaTecnicaDbContext _context;

        public OrdenesService(PruebaTecnicaDbContext context)
        {
            _context = context;
        }

        public async Task<IngresoOrdenDTO> CrearNuevaOrden(IngresoOrdenDTO ordenDTO)
        {
            var nuevaOrden = new OrdenCompra
            {
                Cliente = ordenDTO.Cliente,
                FechaCreacion = DateTime.Now,
                Total = ordenDTO.Total,
                OrdenProductos = new List<OrdenProducto>()
            };

            foreach (var productoDTO in ordenDTO.listaProductos)
            {
                var productoAgregado = await _context.productos
                    .Where(p => p.Nombre == productoDTO.Nombre)
                    .Select(p => p.IdProducto)
                    .FirstOrDefaultAsync();

                var productoRequerido = await _context.productos
                    .FindAsync(productoAgregado);

                nuevaOrden.OrdenProductos.Add(new OrdenProducto
                {
                    IdOrden = nuevaOrden.IdOrden,
                    IdProducto = productoAgregado,
                    OrdenCompra = nuevaOrden,
                    Producto = productoRequerido
                });
            }

            _context.ordenCompras.Add(nuevaOrden);
            await _context.SaveChangesAsync();

            return ordenDTO;
        }


        public IEnumerable<IngresoOrdenDTO> TraerTodasOrdenes()
        {
            var listarOrdenes = _context.ordenCompras
                .Include(op => op.OrdenProductos)
                .ThenInclude(p => p.Producto)
                .Select(pc => new IngresoOrdenDTO
                {
                    Cliente = pc.Cliente,
                    listaProductos = pc.OrdenProductos
                        .Where(op => op.IdOrden == pc.IdOrden && op.IdProducto == op.Producto.IdProducto)
                        .Select(p => new IngresoProductoDTO
                        {
                            Nombre = p.Producto.Nombre,
                            Precio = p.Producto.Precio
                        }).ToList(),
                    Total = pc.Total
                }).ToList();
            return listarOrdenes;
        }

        public async Task<IngresoOrdenDTO> ObtenerUnaOrdenPorId(int id)
        {
            var ordenPorId = await _context.ordenCompras
                .Where(pc => pc.IdOrden == id)
                .Include(op => op.OrdenProductos)
                .ThenInclude(p => p.Producto)
                .Select(pc => new IngresoOrdenDTO
                {
                    Cliente = pc.Cliente,
                    listaProductos = pc.OrdenProductos
                        .Where(op => op.IdOrden == pc.IdOrden && op.IdProducto == op.Producto.IdProducto)
                        .Select(p => new IngresoProductoDTO
                        {
                            Nombre = p.Producto.Nombre,
                            Precio = p.Producto.Precio
                        }).ToList(),
                    Total = pc.Total
                }).FirstOrDefaultAsync();

            return ordenPorId;
        }

        public async Task<IngresoOrdenDTO> ModificarOrdenPorId(int id, IngresoOrdenDTO ordenDTO)
        {
            // 1. Buscar la orden existente incluyendo sus productos
            var ordenExistente = await _context.ordenCompras
                .Include(o => o.OrdenProductos)
                .ThenInclude(op => op.Producto)
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (ordenExistente == null)
            {
                return null; // Orden no encontrada
            }

            // 2. Actualizar campos básicos de la orden
            ordenExistente.Cliente = ordenDTO.Cliente;
            ordenExistente.Total = ordenDTO.Total;

            // 3. Eliminar relaciones antiguas (productos anteriores)
            _context.ordenProductos.RemoveRange(ordenExistente.OrdenProductos);

            // 4. Agregar nuevas relaciones (productos actualizados)
            foreach (var productoDTO in ordenDTO.listaProductos)
            {
                var productoExistente = await _context.productos
                    .FirstOrDefaultAsync(p => p.Nombre == productoDTO.Nombre);

                if (productoExistente != null)
                {
                    ordenExistente.OrdenProductos.Add(new OrdenProducto
                    {
                        IdOrden = ordenExistente.IdOrden,
                        IdProducto = productoExistente.IdProducto,
                        OrdenCompra = ordenExistente,
                        Producto = productoExistente
                    });
                }
            }

            // 5. Guardar cambios
            await _context.SaveChangesAsync();

            // 6. Retornar el DTO recibido (coherente con tu enfoque)
            return ordenDTO;
        }

        public async Task<IngresoOrdenDTO> EliminarOrdenPorId(int id)
        {
            var ordenAEliminar = await _context.ordenCompras
                .FirstOrDefaultAsync(pc => pc.IdOrden == id);

            if (ordenAEliminar != null)
            {
                _context.ordenCompras.Remove(ordenAEliminar);
            }

            await _context.SaveChangesAsync();

            return null;
        }
    }
}