using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Zona
    {
        public Zona()
        {
            Regiones = new HashSet<Regione>();
        }

        public int IdZona { get; set; }
        public string SZona { get; set; } = null!;

        public virtual ICollection<Regione> Regiones { get; set; }
    }
}
