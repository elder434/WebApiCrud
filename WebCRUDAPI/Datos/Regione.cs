using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Regione
    {
        public Regione()
        {
            Comunas = new HashSet<Comuna>();
        }

        public int IdRegion { get; set; }
        public int IdZona { get; set; }
        public string SRegion { get; set; } = null!;
        public int? IOrden { get; set; }

        public virtual Zona IdZonaNavigation { get; set; } = null!;
        public virtual ICollection<Comuna> Comunas { get; set; }
    }
}
