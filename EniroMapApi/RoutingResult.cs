using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi
{

    public partial class RoutingResult
    {
        [JsonProperty("total-length")]
        public double TotalLength { get; set; }

        [JsonProperty("total-duration")]
        public long TotalDuration { get; set; }

        [JsonProperty("ferry-time")]
        public long FerryTime { get; set; }

        [JsonProperty("route-instructions")]
        public object[] RouteInstructions { get; set; }
    }

    public partial class RoutingResult
    {
        public static RoutingResult FromJson(string json) => JsonConvert.DeserializeObject<RoutingResult>(json, RoutingResultConverter.Settings);
    }

    internal static class RoutingResultConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { 
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
