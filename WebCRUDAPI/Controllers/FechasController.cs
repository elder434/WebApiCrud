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
    public class FechasController : ControllerBase
    {
        private readonly IFechaServices _services;

        public FechasController(IFechaServices services)
        {
            this._services = services;
        }

        /************************************* |listo|  *************************************/

        [HttpPost("vecesUsadosCasilleroPorFecha")]
        public async Task<ActionResult> getByFecha([FromBody] fechas date)
        {

            var list = await _services.CasillasUsadas(date);

            if (list.Count > 0)
            {
                return Ok(list);
            }

            if(list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
        /************************************* |listo|  *************************************/

        [HttpPost("clienteRutNombreQueMasUsoElCasillero")]
        public async Task<ActionResult> Get([FromBody] fechas date)
        {
            var list = await _services.GetByUsuarioMasUso(date);

            if (list.Count > 0)
            {
                return Ok(list);
            }

            if (list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }


        /************************************* |listo|  *************************************/


        [HttpPost("clienteRutNombreQueMenosUsoElCasillero")]
        public async Task<ActionResult> GetByCasilleroMenor([FromBody] fechas date)
        {
            var list = await _services.GetByUsuarioMenosUso(date);

            if (list.Count > 0)
            {
                return Ok(list);
            }

            if (list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /************************************* |listo|  *************************************/




        [HttpPost("pedidosCompleto")]
        public async Task<ActionResult> PostCompleto([FromBody] fechas date)
        {
            var list = await _services.PedidosCompleto(date);

            if (list.Count > 0)
            {
                return Ok(list);
            }

            if (list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /************************************* |listo|  *************************************/


        [HttpPost("pedidosPorCompletados")]
        public async Task<ActionResult> PostPorCompletados([FromBody] fechas date)
        {

            var list = await _services.PedidosPorCompletados(date);
            if (list.Count > 0)
            {
                return Ok(list);
            }

            if (list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /************************************* |listo|  *************************************/

        [HttpPost("pedidosSinCompletados")]
        public async Task<ActionResult> PostSinCompletados([FromBody] fechas date)
        {

            var list = await _services.PedidosSinCompletados(date);
            if (list.Count > 0)
            {
                return Ok(list);
            }

            if (list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }



        /************************************* |listo|  *************************************/

        [HttpPost("mostarElPedidoMasRapido")]
        public async Task<ActionResult> PedidoRapido([FromBody] fechas date)
        {
            var list = await _services.PedidoMasRapido(date);

            if (list.Count > 0)
            {
                return Ok(list);
            }

            if (list == null)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
