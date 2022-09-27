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

        [HttpGet("convertir imagen base64")]
        public ActionResult ConvertirBase64()
        {
            var cadena = _services.GetImagenBase64();

            return Ok(cadena);
        }
    }
}
