using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public string SDniCli { get; set; } = null!;
        public string SNombre { get; set; } = null!;
        public string? STelefono { get; set; }
        public string? SCorreo { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
