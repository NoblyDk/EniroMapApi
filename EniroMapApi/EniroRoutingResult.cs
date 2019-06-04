using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi
{
    public partial class EniroRoutingResult
    {
        [JsonProperty("total-length", NullValueHandling = NullValueHandling.Ignore)]
        public double TotalLength { get; set; }

        [JsonProperty("total-duration", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalDuration { get; set; }

        [JsonProperty("ferry-time", NullValueHandling = NullValueHandling.Ignore)]
        public int FerryTime { get; set; }
    }
}
