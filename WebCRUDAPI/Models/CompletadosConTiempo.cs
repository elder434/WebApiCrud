namespace WebCRUDAPI.Models
{
    public class CompletadosConTiempo
    {
        public int Oficina { get; set; }
        public int Locker { get; set; }
        public int Casillero { get; set; }
        public DateTime Registrado { get; set; }
        public DateTime PedidosEntregados { get; set; }
    }
}
