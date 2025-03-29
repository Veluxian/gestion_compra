using Prueba_Tecnica.DTO;

namespace Prueba_Tecnica.Interfaces
{
    public interface IOrdenesInterface
    {
        Task<IngresoOrdenDTO> CrearNuevaOrden(IngresoOrdenDTO ordenDTO);
        IEnumerable<IngresoOrdenDTO> TraerTodasOrdenes();
        Task<IngresoOrdenDTO> ObtenerUnaOrdenPorId(int IdOrden);
        Task<IngresoOrdenDTO> ModificarOrdenPorId(int IdOrden);
        Task<IngresoOrdenDTO> EliminarOrdenPorId(int IdOrden);
    }
}