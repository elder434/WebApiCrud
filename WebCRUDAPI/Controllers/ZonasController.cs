using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCRUDAPI.Datos;
using WebCRUDAPI.Models;
using WebCRUDAPI.Services.Interfaces;

namespace WebCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZonasController : ControllerBase
    {
        private readonly IZonasServices _services;
        public static readonly NLog.ILogger _log = NLog.LogManager.GetCurrentClassLogger();

        public ZonasController(IZonasServices services)
        {
            this._services = services;
        }

        [HttpGet("mostrarZonas")]
        public async Task<ActionResult<List<Zona>>> Get()
        {
            var zona = await _services.GetZonasToList();

            if(zona == null)
            {
                return NotFound("ERROR");
            }

            return zona;
        }

        [HttpGet("mostrarRegionDeZonas/{id:int}")]
        public async Task<IActionResult> GetRegionesByZona(int id)
        {
            var zona = await _services.GetZonaConRegiones(id);
            if (zona == null)
            {
                return BadRequest("No existe la zona con ese id");
            }
            return Ok(zona);
        }

        [HttpPost("agregarZona")]
        public async Task<ActionResult> Post(ZonaAgregar zonaAgregar)
        {
            await _services.AgregarZona(zonaAgregar);

            return Ok();
        }

        [HttpPut("editarZona/{id:int}")]
        public async Task<ActionResult>  Put(int id, ZonaAgregar zonaAgregar)
        {
            var zonaToUpadate = await _services.EditarZona(id, zonaAgregar);
            if (zonaToUpadate == null)
            {
                return BadRequest("No existe la zona con ese id");
            }

            return Ok();
        }

        [HttpDelete("eliminarZona/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var zona = await _services.EliminarZona(id);
            if (zona == null)
            {
                return BadRequest("No existe la zona con ese id");
            }

            return Ok();
        }
    }
}
