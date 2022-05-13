using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoBL.Models
{
    public partial class Encargado
    {
        public Encargado()
        {
            Requerimientos = new HashSet<Requerimiento>();
        }

        public int? IdEncargado { get; set; }
        public string NombreEncargado { get; set; }

        public virtual ICollection<Requerimiento> Requerimientos { get; set; }
    }
}
