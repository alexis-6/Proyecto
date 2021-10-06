﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoBL.DTOs
{
    public class RequerimientoDTO
    {
        [JsonProperty("IdRequerimiento")]
        public int IdRequerimiento { get; set; }
        [Required]

        [JsonProperty("IdArea")]
        public int IdArea { get; set; }
        [Required]

        [JsonProperty("IdEncargado")]
        public int IdEncargado { get; set; }
        [Required]

        [JsonProperty("IdPrioridad")]
        public int IdPrioridad { get; set; }
        [Required]

        [JsonProperty("Aplicativo")]
        public string Aplicativo { get; set; }
        [Required]

        [JsonProperty("Alcance")]
        public string Alcance { get; set; }
        [Required]

        [JsonProperty("FechaSolicitud")]
        public DateTime FechaSolicitud { get; set; }

        [JsonProperty("FechaDesarrollo")]
        public DateTime FechaDesarrollo { get; set; }

        [JsonProperty("DiasDesarrollo")]
        [Required]
        public int DiasDesarrollo { get; set; }

        [JsonProperty("FechaPrueba")]
        public DateTime FechaPrueba { get; set; }
    }
    public class RequerimientoOutputDTO
    {
        public RequerimientoOutputDTO()
        {
            DArea = new AreaOutputDTO();
            NombreEncargado = new EncargadoOutputDTO();
            DPrioridad = new PrioridadOutputDTO();
        }

        [JsonProperty("IdRequerimiento")]
        public int IdRequerimiento { get; set; }

        [JsonProperty("IdArea")]
        public int IdArea { get; set; }

        [JsonProperty("IdEncargado")]
        public int IdEncargado { get; set; }

        [JsonProperty("IdPrioridad")]
        public int IdPrioridad { get; set; }

        [JsonProperty("Aplicativo")]
        public string Aplicativo { get; set; }

        [JsonProperty("Alcance")]
        public string Alcance { get; set; }

        [JsonProperty("FechaSolicitud")]
        public DateTime FechaSolicitud { get; set; }

        [JsonProperty("FechaDesarrollo")]
        public DateTime FechaDesarrollo { get; set; }

        [JsonProperty("DiasDesarrollo")]
        public int DiasDesarrollo { get; set; }

        [JsonProperty("FechaPrueba")]
        public DateTime FechaPrueba { get; set; }

        [JsonProperty("DArea")]
        public AreaOutputDTO DArea { get; set; }

        [JsonProperty("NombreEncargado")]
        public EncargadoOutputDTO NombreEncargado { get; set; }

        [JsonProperty("DPrioridad")]
        public PrioridadOutputDTO DPrioridad { get; set; }
    }
}
