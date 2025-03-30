using Prueba_Tecnica.DTO;
using Prueba_Tecnica.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Prueba_Tecnica.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoInterface _ProductoService;

        public ProductoController(IProductoInterface ProductoService)
        {
            _ProductoService = ProductoService;
        }

        [HttpPost("crearproducto")]
        public async Task<ActionResult<IngresoProductoDTO>> CrearNuevoProducto(IngresoProductoDTO productoDTO)
        {
            var nuevoProducto = await _ProductoService.CrearNuevoProducto(productoDTO);
            return nuevoProducto;
        }

        [HttpGet("listarproductos")]
        public ActionResult<IEnumerable<IngresoProductoDTO>> ListarTodosProductos()
        {
            var listarProductos = _ProductoService.ListarTodosProductos();
            return Ok(listarProductos);
        }
    }
}
