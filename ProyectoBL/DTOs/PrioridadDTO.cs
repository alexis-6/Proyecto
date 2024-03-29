﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoBL.DTOs
{
    public class PrioridadDTO
    {
        [JsonProperty("IdPrioridad")]
        public int? IdPrioridad { get; set; }

        [JsonProperty("DPrioridad")]
        [Required(ErrorMessage = "El campo DPrioridad es requerido")]
        [StringLength(10)]
        public string DPrioridad { get; set; }
    }
    public class PrioridadOutputDTO
    {
        [JsonProperty("IdPrioridad")]
        public int IdPrioridad { get; set; }

        [JsonProperty("DPrioridad")]
        public string DPrioridad { get; set; }
    }
}
