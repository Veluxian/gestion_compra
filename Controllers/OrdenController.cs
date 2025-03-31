using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Prueba_Tecnica.Models;

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
            if (ordenDTO == null)
            {
                return BadRequest("Los Datos de la orden no son validos");
            }
            var nuevaOrden = await _OrdenesService.CrearNuevaOrden(ordenDTO);
            return Ok(nuevaOrden);
        }

        [HttpGet("listar")]
        public ActionResult<IEnumerable<IngresoNuevaOrden>> TraerTodasOrdenes()
        {
            var listarOrdenes = _OrdenesService.TraerTodasOrdenes();
            return Ok(listarOrdenes);
        }

        [HttpGet("traer{id}")]
        public async Task<ActionResult<IngresoNuevaOrden?>> ObtenerUnaOrdenPorId(int id)
        {
            var ordenPorId = await _OrdenesService.ObtenerUnaOrdenPorId(id);
            if (ordenPorId == null)
            {
                return NotFound($"no se encuentra la orden {id} ya que no existe");
            }
            return Ok(ordenPorId);
        }

        [HttpPut("modificar{id}")]
        public async Task<ActionResult<IngresoOrdenDTO>> ModificarOrdenPorId(int id, IngresoOrdenDTO ordenDTO)
        {
            if (ordenDTO == null)
            {
                return BadRequest("los datos de la orden son invalidos");
            }
            var ordenModificada = await _OrdenesService.ModificarOrdenPorId(id, ordenDTO);
            if (ordenModificada == null)
            {
                return NotFound($"no se encontró la orden con Id {id}");
            }
            return Ok(ordenModificada);
        }

        [HttpDelete("borrar{id}")]
        public async Task<ActionResult<IngresoOrdenDTO>> EliminarOrdenPorId(int id)
        {
            var ordenEliminada = await _OrdenesService.EliminarOrdenPorId(id);
            if (ordenEliminada == null)
            {
                return NotFound($"No se encontró la orden con ID {id}.");
            }
            return NoContent();
        }
    }
}