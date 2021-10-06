using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoBL.Models
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Requerimientos = new HashSet<Requerimiento>();
        }

        public int IdPrioridad { get; set; }
        public string DPrioridad { get; set; }

        public virtual ICollection<Requerimiento> Requerimientos { get; set; }
    }
}
