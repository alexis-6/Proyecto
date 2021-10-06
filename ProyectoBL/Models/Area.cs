using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoBL.Models
{
    public partial class Area
    {
        public Area()
        {
            Requerimientos = new HashSet<Requerimiento>();
        }

        public int IdArea { get; set; }
        public string DArea { get; set; }

        public virtual ICollection<Requerimiento> Requerimientos { get; set; }
    }
}
