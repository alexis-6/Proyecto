using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBL.DTOs
{
    public class RespuestaRequerimientoDTO
    {
        [JsonProperty("Code")]
        public int Code { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Data")]
        public dynamic Data { get; set; }

    }
}
