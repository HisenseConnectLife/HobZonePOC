using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HobZonePOC.Models
{
    public partial class ZoneInfo
    {
        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("SL1")]
        public Zone Zone { get; set; }

        [JsonProperty("SL2")]
        public Zone Zone2 { get; set; }

        [JsonProperty("SL3")]
        public Zone Zone3 { get; set; }

        [JsonProperty("SL4")]
        public Zone Zone4 { get; set; }

        [JsonProperty("SL5")]
        public Zone Zone5 { get; set; }

        [JsonProperty("SL6")]
        public Zone Zone6 { get; set; }

    }

    public partial class Zone
    {
        [JsonProperty("present")]
        public bool Present { get; set; }

        [JsonProperty("shape")]
        public string Shape { get; set; }

        [JsonProperty("x")]
        public int? X { get; set; }

        [JsonProperty("y")]
        public int? Y { get; set; }

        [JsonProperty("w")]
        public int? W { get; set; }

        [JsonProperty("h")]
        public int? H { get; set; }
    }
}
