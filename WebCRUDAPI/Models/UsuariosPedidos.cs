namespace WebCRUDAPI.Models
{
    public class UsuariosPedidos
    {
        public int Locker { get; set; }
        public int Oficina { get; set; }
        public int Casillero { get; set; }
        public int VecesUsado { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }

        public static implicit operator int(UsuariosPedidos v)
        {
            throw new NotImplementedException();
        }
    }
}
