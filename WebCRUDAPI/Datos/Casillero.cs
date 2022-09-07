using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Casillero
    {
        public Casillero()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdLocker { get; set; }
        public int IdOficina { get; set; }
        public int IdCasillero { get; set; }
        public string? STipo { get; set; }
        public int? IAlto { get; set; }
        public int? IAncho { get; set; }
        public int? ILargo { get; set; }
        public string? SEstado { get; set; }

        public virtual Locker IdLockerNavigation { get; set; } = null!;
        public virtual Oficina IdOficinaNavigation { get; set; } = null!;
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
