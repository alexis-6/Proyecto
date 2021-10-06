using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBL.DTOs
{
    public class RespuestaAreaDTO
    {
        [JsonProperty("IdArea")]
        public int IdArea { get; set; }

        [JsonProperty("DArea")]
        public string DArea { get; set; }

        [JsonProperty("Data")]
        public dynamic Data { get; set; }
    }
}
