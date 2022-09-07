using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Usuario
    {
        public Usuario()
        {
            Pedidos = new HashSet<Pedido>();
            Usuariooficinas = new HashSet<Usuariooficina>();
        }

        public string SDniUsu { get; set; } = null!;
        public string? SNombreUsu { get; set; }
        public string? SPassUsu { get; set; }
        public bool? BHab { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<Usuariooficina> Usuariooficinas { get; set; }
    }
}
