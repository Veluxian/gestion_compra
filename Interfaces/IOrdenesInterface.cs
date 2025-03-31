using Prueba_Tecnica.DTO;

namespace Prueba_Tecnica.Interfaces
{
    public interface IOrdenesInterface
    {
        Task<IngresoOrdenDTO> CrearNuevaOrden(IngresoOrdenDTO ordenDTO);
        IEnumerable<IngresoNuevaOrden> TraerTodasOrdenes();
        Task<IngresoNuevaOrden> ObtenerUnaOrdenPorId(int IdOrden);
        Task<IngresoOrdenDTO> ModificarOrdenPorId(int IdOrden, IngresoOrdenDTO ordenDTO);
        Task<IngresoOrdenDTO> EliminarOrdenPorId(int IdOrden);
    }
}