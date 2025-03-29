using Prueba_Tecnica.Data;
using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;

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
            return null;
        }

        public IEnumerable<IngresoOrdenDTO> TraerTodasOrdenes()
        {
            var listarOrdenes = _context.ordenCompras
                .Select(pc => new IngresoOrdenDTO
                {
                    Cliente = pc.Cliente,
                    Total = pc.Total
                }).ToList();
            return listarOrdenes;
        }

        public Task<IngresoOrdenDTO> ObtenerUnaOrdenPorId(int id)
        {
            return null;
        }

        public Task<IngresoOrdenDTO> ModificarOrdenPorId(int id)
        {
            return null;
        }

        public Task<IngresoOrdenDTO> EliminarOrdenPorId(int id)
        {
            return null;
        }
    }
}
