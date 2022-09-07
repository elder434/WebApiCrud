using WebCRUDAPI.Datos;
using WebCRUDAPI.Models;

namespace WebCRUDAPI.Services.Interfaces
{
    public interface IZonasServices
    {
        public Task<List<Zona>> GetZonasToList();
        public Task<ZonaConRegiones> GetZonaConRegiones(int id);
        public Task<Zona> AgregarZona(ZonaAgregar zonaAgregar);
        public Task<Zona> EditarZona(int id, ZonaAgregar zonaAgregar);
        public Task<Zona> EliminarZona(int id);
    }
}
