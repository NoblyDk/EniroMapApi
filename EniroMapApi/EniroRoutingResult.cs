using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi
{
    public partial class EniroRoutingResult
    {
        [JsonProperty("route-geometries", NullValueHandling = NullValueHandling.Ignore)]
        public RouteGeometries RouteGeometries { get; set; }

        public double TotalLength
        {
            get => RouteGeometries?.Features?.FirstOrDefault()?.Properties?.Length ?? 0;
        }

        [JsonProperty("total-duration", NullValueHandling = NullValueHandling.Ignore)]
        public int TotalDuration { get; set; }

        [JsonProperty("ferry-time", NullValueHandling = NullValueHandling.Ignore)]
        public int FerryTime { get; set; }
    }

    public partial class RouteGeometries
    {
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public List<RouteGeometriesFeature> Features { get; set; }
    }

    public partial class RouteGeometriesFeature
    {
        [JsonProperty("properties", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleProperties Properties { get; set; }
    }

    public partial class PurpleProperties
    {
        [JsonProperty("length", NullValueHandling = NullValueHandling.Ignore)]
        public long? Length { get; set; }
    }
}
