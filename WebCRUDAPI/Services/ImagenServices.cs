using Newtonsoft.Json;
using NLog.Fluent;
using System;
using System.Text;
using WebCRUDAPI.Datos;
using WebCRUDAPI.Services.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace WebCRUDAPI.Services
{
    public class ImagenServices : IImagenService
    {
        private readonly IWebHostEnvironment _env;
        private readonly lockersqaContext _context;

        public ImagenServices(IWebHostEnvironment env, lockersqaContext context)
        {
            _env = env;
            _context = context;
        }

        public string GetImagenBase64()
        {
            var path = Path.Combine(_env.ContentRootPath, "imagenes", "descargar.jpg");

            var img =  File.ReadAllBytes(path);

            var cadena = Convert.ToBase64String(img);

            return cadena;
        }

        public async Task<List<Zona>> InsertExcel()
        {
            //Global.HostEnvironment.ContentRootPath obtiene la ruta actual en donde esta posicionada la aplicación
            var path = Path.Combine(_env.ContentRootPath, "imagenes", "zonas.csv"); //creo la ruta en donde guardare el archivo
            try
            {
                
                System.IO.StreamReader archivo = new System.IO.StreamReader(path, Encoding.GetEncoding(28591)); //Abro lectura del archivo creado con codificación en español
                var lista = new List<Zona>();
                var texto = archivo.ReadToEnd(); //Lee archivo en forma de texto hasta el final
                archivo.Close();
                var fila = texto.Replace("\r\n", ";").TrimEnd(';').Split(";"); //Reemplazo los caracteres \r\n por ";" para luego eliminar el ultimo ";" y generar un array por cada vez que se encuentre el punto y coma
                int rowCount = fila.Length / 2; //Divido el largo del array por la cantidad de columnas del .csv (3) para obtener la cantidad de filas a insertar
                for (int i = 0; i < (rowCount - 1); i++) //(rowCount - 1) ya que la primera fila del .csv corresponde a los titulos
                {
                    var fakeIndex = i * 2; //Multiplico "i" por la catidad de columnas que contiene el archivo .csv para obtener la posición de cada casilla
                    var insert = new Zona()
                    {
                        IdZona = int.Parse(fila[2 + fakeIndex]),
                        SZona = fila[(3 + fakeIndex)], //Obtiene el valor de fila[casilla(3) + fakeIndex] asi cuando i corresponda a 2 obtendra el valor de 9 (fila[3 + 6])
                    };
                    lista.Add(insert); //Guardo el objeto a insertar en una lista
                }
                
                
                await _context.AddRangeAsync(lista);
                var guardado = await _context.SaveChangesAsync();
                if(guardado == 0)
                {
                    return null;
                }


                return lista;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }



        public async Task<string> DeletetExcel()
        {
            //Global.HostEnvironment.ContentRootPath obtiene la ruta actual en donde esta posicionada la aplicación
            var path = Path.Combine(_env.ContentRootPath, "imagenes", "zonas.csv"); //creo la ruta en donde guardare el archivo
            var mensaje = "";
            try
            {

                System.IO.StreamReader archivo = new System.IO.StreamReader(path, Encoding.GetEncoding(28591)); //Abro lectura del archivo creado con codificación en español
                var lista = new List<Zona>();
                var texto = archivo.ReadToEnd(); //Lee archivo en forma de texto hasta el final
                archivo.Close();
                var fila = texto.Replace("\r\n", ";").TrimEnd(';').Split(";"); //Reemplazo los caracteres \r\n por ";" para luego eliminar el ultimo ";" y generar un array por cada vez que se encuentre el punto y coma
                int rowCount = fila.Length / 2; //Divido el largo del array por la cantidad de columnas del .csv (3) para obtener la cantidad de filas a insertar
                for (int i = 0; i < (rowCount - 1); i++) //(rowCount - 1) ya que la primera fila del .csv corresponde a los titulos
                {
                    var fakeIndex = i * 2; //Multiplico "i" por la catidad de columnas que contiene el archivo .csv para obtener la posición de cada casilla
                    var insert = new Zona()
                    {
                        IdZona = int.Parse(fila[2 + fakeIndex]),
                        SZona = fila[(3 + fakeIndex)], //Obtiene el valor de fila[casilla(3) + fakeIndex] asi cuando i corresponda a 2 obtendra el valor de 9 (fila[3 + 6])
                    };
                    lista.Add(insert); //Guardo el objeto a insertar en una lista
                }


                _context.RemoveRange(lista);
                var guardado = await _context.SaveChangesAsync();
                if (guardado == 0)
                {
                    mensaje = "no se pudo eliminado exitozamente";

                    return mensaje;
                }

                mensaje = "eliminado exitozamente";

                return mensaje;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
