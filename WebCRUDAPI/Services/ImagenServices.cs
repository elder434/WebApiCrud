using System;
using WebCRUDAPI.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace WebCRUDAPI.Services
{
    public class ImagenServices : IImagenService
    {
        private readonly IWebHostEnvironment _env;

        public ImagenServices(IWebHostEnvironment env)
        {
            _env = env;
        }

        public string GetImagenBase64()
        {
            var path = Path.Combine(_env.ContentRootPath, "imagenes", "descargar.jpg");

            var img =  File.ReadAllBytes(path);

            var cadena = Convert.ToBase64String(img);

            return cadena;
        }
    }
}
