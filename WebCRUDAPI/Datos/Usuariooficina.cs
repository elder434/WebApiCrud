using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Usuariooficina
    {
        public int IdOficina { get; set; }
        public string SDniUsu { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Oficina IdOficinaNavigation { get; set; } = null!;
        public virtual Usuario SDniUsuNavigation { get; set; } = null!;
    }
}
