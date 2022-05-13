using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoBL.DTOs
{
    public class EncargadoDTO
    {
        [JsonProperty("IdEncargado")]
        public int? IdEncargado { get; set; }

        [JsonProperty("NombreEncargado")]
        [Required(ErrorMessage = "El campo NombreEncargado es requerido")]
        [StringLength(30)]
        public string NombreEncargado { get; set; }
    }
    public class EncargadoOutputDTO
    {
        [JsonProperty("IdEncargado")]
        public int IdEncargado { get; set; }

        [JsonProperty("NombreEncargado")]
        public string NombreEncargado { get; set; }
    }
}
