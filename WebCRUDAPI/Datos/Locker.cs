using System;
using System.Collections.Generic;

namespace WebCRUDAPI.Datos
{
    public partial class Locker
    {
        public Locker()
        {
            Casilleros = new HashSet<Casillero>();
        }

        public int IdLocker { get; set; }
        public sbyte? INumPuertas { get; set; }
        public bool? BHab { get; set; }
        public string? SNomLocker { get; set; }
        public string? SDireccion { get; set; }
        public int? IdComuna { get; set; }
        public string? SLatitud { get; set; }
        public string? SLongitud { get; set; }
        public sbyte ILuhi { get; set; }
        public sbyte ILumi { get; set; }
        public sbyte ILuhf { get; set; }
        public sbyte ILumf { get; set; }
        public sbyte IMahi { get; set; }
        public sbyte IMami { get; set; }
        public sbyte IMahf { get; set; }
        public sbyte IMamf { get; set; }
        public sbyte IMihi { get; set; }
        public sbyte IMimi { get; set; }
        public sbyte IMihf { get; set; }
        public sbyte IMimf { get; set; }
        public sbyte IJuhi { get; set; }
        public sbyte IJumi { get; set; }
        public sbyte IJuhf { get; set; }
        public sbyte IJumf { get; set; }
        public sbyte IVihi { get; set; }
        public sbyte IVimi { get; set; }
        public sbyte IVihf { get; set; }
        public sbyte IVimf { get; set; }
        public sbyte ISahi { get; set; }
        public sbyte ISami { get; set; }
        public sbyte ISahf { get; set; }
        public sbyte ISamf { get; set; }
        public sbyte IDohi { get; set; }
        public sbyte IDomi { get; set; }
        public sbyte IDohf { get; set; }
        public sbyte IDomf { get; set; }

        public virtual Comuna? IdComunaNavigation { get; set; }
        public virtual ICollection<Casillero> Casilleros { get; set; }
    }
}
