using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Exceptions;
using Prueba_Tecnica.Funciones;
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
            if (ordenDTO == null || ordenDTO.listaProductosId == null || !ordenDTO.listaProductosId.Any())
            {
                throw new BadRequestException("La orden debe contener productos.");
            }

            var nuevaOrden = new OrdenCompra
            {
                Cliente = ordenDTO.Cliente,
                FechaCreacion = DateTime.Now,
                Total = 0.0, 
                OrdenProductos = new List<OrdenProducto>()
            };

            double totalCalculado = 0.0;
            var conteoMultiple = new HashSet<string>();

            foreach (var productoOrden in ordenDTO.listaProductosId)
            {
                conteoMultiple.Add(productoOrden.ToString());

                var productoExistente = await _context.productos
                    .FirstOrDefaultAsync(p => p.IdProducto == productoOrden);
                if (productoExistente == null)
                {
                    throw new NotFoundException($"el producto con id '{productoOrden}' no existe");
                }

                totalCalculado += productoExistente.Precio;

                nuevaOrden.OrdenProductos.Add(new OrdenProducto
                {
                    OrdenCompra = nuevaOrden,
                    Producto = productoExistente,
                });
            }
            var variado = conteoMultiple.Count;

            nuevaOrden.Total = DescuentoGrandesCompras.AplicarDescuentoGranCompra(totalCalculado);
            nuevaOrden.Total = DescuentoVariedad.AplicarDescuentoVariedad(totalCalculado, variado);

            _context.ordenCompras.Add(nuevaOrden);
            await _context.SaveChangesAsync();

            ordenDTO.Total = totalCalculado;

            return ordenDTO;
        }

        public IEnumerable<IngresoNuevaOrden> TraerTodasOrdenes()
        {
            var listarOrdenes = _context.ordenCompras
                .Include(op => op.OrdenProductos)
                .ThenInclude(p => p.Producto)
                .Select(pc => new IngresoNuevaOrden
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

        public async Task<IngresoNuevaOrden> ObtenerUnaOrdenPorId(int id)
        {
            var ordenPorId = await _context.ordenCompras
                .Where(pc => pc.IdOrden == id)
                .Include(op => op.OrdenProductos)
                .ThenInclude(p => p.Producto)
                .Select(pc => new IngresoNuevaOrden
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
            if (ordenPorId == null)
            {
                throw new NotFoundException($"no se encontro la orden con Id {id}");
            }

            return ordenPorId;
        }

        public async Task<IngresoOrdenDTO> ModificarOrdenPorId(int id, IngresoOrdenDTO ordenDTO)
        {
            var ordenExistente = await _context.ordenCompras
                .Include(o => o.OrdenProductos)
                .ThenInclude(op => op.Producto)
                .FirstOrDefaultAsync(o => o.IdOrden == id);

            if (ordenExistente == null)
            {
                throw new NotFoundException($"no se encontró la orden con Id {id}");
            }

            ordenExistente.Cliente = ordenDTO.Cliente;

            _context.ordenProductos.RemoveRange(ordenExistente.OrdenProductos);

            double totalCalculado = 0.0;
            foreach (var productosOrden in ordenDTO.listaProductosId)
            {
                var productoExistente = await _context.productos
                    .FirstOrDefaultAsync(p => p.IdProducto == productosOrden);

                if (productoExistente == null)
                {
                    throw new NotFoundException($"El producto con id'{productosOrden}' no existe");
                }

                totalCalculado += productoExistente.Precio;

                ordenExistente.OrdenProductos.Add(new OrdenProducto
                {
                    OrdenCompra = ordenExistente,
                    Producto = productoExistente
                });
            }

            ordenExistente.Total = totalCalculado;

            await _context.SaveChangesAsync();

            ordenDTO.Total = totalCalculado;

            return ordenDTO;
        }

        public async Task<IngresoOrdenDTO> EliminarOrdenPorId(int id)
        {
            var ordenAEliminar = await _context.ordenCompras
                .FirstOrDefaultAsync(pc => pc.IdOrden == id);
            if (ordenAEliminar == null)
            {
                throw new NotFoundException($"no se puede eliminar: la orden con Id {id} no existe");
            }
            _context.ordenCompras.Remove(ordenAEliminar);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}