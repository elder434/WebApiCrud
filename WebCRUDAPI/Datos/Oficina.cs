using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Oficina
    {
        public Oficina()
        {
            Casilleros = new HashSet<Casillero>();
            Pedidos = new HashSet<Pedido>();
            Usuariooficinas = new HashSet<Usuariooficina>();
        }

        public int IdOficina { get; set; }
        public int IdComuna { get; set; }
        public string? SCodOfi { get; set; }
        public int IHorVencCarga { get; set; }
        public int IHorVencRetiro { get; set; }
        public string SCorreo { get; set; } = null!;
        public string SOficina { get; set; } = null!;
        public bool? BEstado { get; set; }

        public virtual Comuna IdComunaNavigation { get; set; } = null!;
        public virtual ICollection<Casillero> Casilleros { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Usuariooficina> Usuariooficinas { get; set; }
    }
}
