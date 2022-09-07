namespace WebCRUDAPI.Models
{
    public class Entregados
    {
        public int Oficina { get; set; }
        public int Locker { get; set; }
        public int Casillero { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaEntregado { get; set; }
    }
}
