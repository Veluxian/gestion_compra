using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Prueba_Tecnica.Controllers
{
    [ApiController]
    [Route("api/ordenes")]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenesInterface _OrdenesService;

        public OrdenController(IOrdenesInterface OrdenesService)
        {
            _OrdenesService = OrdenesService;
        }

        [HttpPost("crearorden")]
        public async Task<ActionResult<IngresoOrdenDTO>> CrearNuevaOrden(IngresoOrdenDTO ordenDTO)
        {
            var nuevaOrden = await _OrdenesService.CrearNuevaOrden(ordenDTO);
            return Ok(nuevaOrden);
        }

        [HttpGet("listar")]
        public ActionResult<IEnumerable<IngresoOrdenDTO>> TraerTodasOrdenes()
        {
            var listarOrdenes = _OrdenesService.TraerTodasOrdenes();
            return Ok(listarOrdenes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngresoOrdenDTO?>> ObtenerUnaOrdenPorId(int id)
        {
            var ordenPorId = await _OrdenesService.ObtenerUnaOrdenPorId(id);
            if (ordenPorId == null)
            {
                return NotFound();
            }
            return (ordenPorId);
        }

        //[HttpPut("{id}")]

        //[HttpDelete("{id}")]
    }
}
