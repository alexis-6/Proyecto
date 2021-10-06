﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProyectoBL.DTOs
{
    public class AreaDTO
    {
        [JsonProperty("IdArea")]
        public int IdArea { get; set; }

        [JsonProperty("DArea")]
        [Required]
        public string DArea { get; set; }
    }
    public class AreaOutputDTO
    {
        [JsonProperty("IdArea")]
        public int IdArea { get; set; }

        [JsonProperty("DArea")]
        public string DArea { get; set; }
    }
}
