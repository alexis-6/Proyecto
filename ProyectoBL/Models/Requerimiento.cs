using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoBL.Models
{
    public partial class Requerimiento
    {
        public int? IdRequerimiento { get; set; }
        public int IdArea { get; set; }
        public int IdEncargado { get; set; }
        public int IdPrioridad { get; set; }
        public string Aplicativo { get; set; }
        public string Alcance { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime FechaDesarrollo { get; set; }
        public int DiasDesarrollo { get; set; }
        public DateTime FechaPrueba { get; set; }

        public virtual Area DArea { get; set; }
        public virtual Encargado NombreEncargado { get; set; }
        public virtual Prioridad DPrioridad { get; set; }
    }
}
