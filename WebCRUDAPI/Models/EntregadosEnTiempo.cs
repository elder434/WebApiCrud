namespace WebCRUDAPI.Models
{
    public class EntregadosEnTiempo
    {
        public int locker { get; set; }
        public int Oficina { get; set; }
        public int Casillero { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string TiempoDentro { get; set; }

    }
}