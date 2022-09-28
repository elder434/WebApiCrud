using WebCRUDAPI.Datos;

namespace WebCRUDAPI.Services.Interfaces
{
    public interface IImagenService
    {
        public string GetImagenBase64();
        public Task<List<Zona>> InsertExcel();
        public Task<string> DeletetExcel();
    }
}
