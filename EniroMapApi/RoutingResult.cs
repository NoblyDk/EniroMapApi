using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi.Routing
{

 public partial class RoutingResult
    {
        [JsonProperty("route-instructions")]
        public RouteInstruction[] RouteInstructions { get; set; }

        [JsonProperty("route-geometries")]
        public RouteGeometries RouteGeometries { get; set; }
    }

    public partial class RouteGeometries
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public RouteGeometriesFeature[] Features { get; set; }

        [JsonProperty("bbox")]
        public string[] Bbox { get; set; }

        [JsonProperty("structuredBBox")]
        public Dictionary<string, double> StructuredBBox { get; set; }
    }

    public partial class RouteGeometriesFeature
    {
        [JsonProperty("geometry")]
        public PurpleGeometry Geometry { get; set; }

        [JsonProperty("properties")]
        public PurpleProperties Properties { get; set; }

        [JsonProperty("bbox")]
        public string[] Bbox { get; set; }

        [JsonProperty("structuredBBox")]
        public Dictionary<string, double> StructuredBBox { get; set; }
    }

    public partial class PurpleGeometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[][][] Coordinates { get; set; }
    }

    public partial class PurpleProperties
    {
        [JsonProperty("route-instruction-id")]
        public string RouteInstructionId { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("length")]
        public long Length { get; set; }
    }

    public partial class RouteInstruction
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("features")]
        public RouteInstructionFeature[] Features { get; set; }

        [JsonProperty("bbox")]
        public string[] Bbox { get; set; }

        [JsonProperty("structuredBBox")]
        public Dictionary<string, double> StructuredBBox { get; set; }
    }

    public partial class RouteInstructionFeature
    {
        [JsonProperty("geometry")]
        public FluffyGeometry Geometry { get; set; }

        [JsonProperty("properties")]
        public FluffyProperties Properties { get; set; }

        [JsonProperty("bbox")]
        public string[] Bbox { get; set; }

        [JsonProperty("structuredBBox")]
        public Dictionary<string, double> StructuredBBox { get; set; }
    }

    public partial class FluffyGeometry
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }

    public partial class FluffyProperties
    {
        [JsonProperty("ienc")]
        public string Ienc { get; set; }

        [JsonProperty("iargs")]
        public Iargs Iargs { get; set; }
    }

    public partial class Iargs
    {
        [JsonProperty("dur")]
        public long Dur { get; set; }

        [JsonProperty("road", NullValueHandling = NullValueHandling.Ignore)]
        public string Road { get; set; }

        [JsonProperty("dist")]
        public long Dist { get; set; }
    }

    public enum TypeEnum { Point };

    public partial class RoutingResult
    {
        public static RoutingResult FromJson(string json) => JsonConvert.DeserializeObject<RoutingResult>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this RoutingResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Point")
            {
                return TypeEnum.Point;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.Point)
            {
                serializer.Serialize(writer, "Point");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
