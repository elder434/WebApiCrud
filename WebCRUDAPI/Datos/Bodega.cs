using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Bodega
    {
        public Bodega()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdBodega { get; set; }
        public string SBodega { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
