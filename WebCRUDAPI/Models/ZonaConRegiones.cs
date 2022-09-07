using WebCRUDAPI.Datos;

namespace WebCRUDAPI.Models
{
    public class ZonaConRegiones
    {
        public int IdZona { get; set; }
        public string SZona { get; set; } = null!;
        public List<string> SRegion { get; set; } = null!;
    }
}
