using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Comuna
    {
        public Comuna()
        {
            Lockers = new HashSet<Locker>();
            Oficinas = new HashSet<Oficina>();
        }

        public int IdComuna { get; set; }
        public int IdRegion { get; set; }
        public string SComuna { get; set; } = null!;

        public virtual Regione IdRegionNavigation { get; set; } = null!;
        public virtual ICollection<Locker> Lockers { get; set; }
        public virtual ICollection<Oficina> Oficinas { get; set; }
    }
}
