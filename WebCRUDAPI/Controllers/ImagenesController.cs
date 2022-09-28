using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCRUDAPI.Services;
using WebCRUDAPI.Services.Interfaces;

namespace WebCRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesController : ControllerBase
    {
        private readonly IImagenService _services;
        public ImagenesController(IImagenService service)
        {
            _services = service;
        }

        [HttpGet("convertirImagenBase64")]
        public ActionResult ConvertirBase64()
        {
            var cadena = _services.GetImagenBase64();

            return Ok(cadena);
        }

        [HttpGet("insertarZonasDelExcel")]
        public async Task<ActionResult> InsertExcel()
        {
            var datos = await _services.InsertExcel();
            return Ok(datos);
        }

        [HttpDelete("eliminarZonasDelExcel")]
        public async Task<ActionResult> DeleteExcel()
        {
            var datos = await _services.DeletetExcel();
            return Ok(datos);
        }
    }
}
