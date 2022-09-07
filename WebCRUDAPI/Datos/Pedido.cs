using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Pedido
    {
        public int Idpedido { get; set; }
        public int IdLocker { get; set; }
        public int IdOficina { get; set; }
        public int IdCasillero { get; set; }
        public string SDniCli { get; set; } = null!;
        public string? SDniUsu { get; set; }
        public string? SPedidoSku { get; set; }
        public DateTime? FechIn { get; set; }
        public DateTime? FechReg { get; set; }
        public DateTime? FechVen { get; set; }
        public DateTime? FechOut { get; set; }
        public string? SCodProd { get; set; }
        public string? SCodQr { get; set; }
        public int ClaveUsuario { get; set; }
        public int ClaveCliente { get; set; }
        public int? IdBodega { get; set; }
        public sbyte BCarga { get; set; }
        public sbyte BRetiroCli { get; set; }

        public virtual Casillero Id { get; set; } = null!;
        public virtual Bodega? IdBodegaNavigation { get; set; }
        public virtual Oficina IdOficinaNavigation { get; set; } = null!;
        public virtual Cliente SDniCliNavigation { get; set; } = null!;
        public virtual Usuario? SDniUsuNavigation { get; set; }
    }
}
