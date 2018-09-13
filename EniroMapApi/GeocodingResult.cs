using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi.Geocoding
{
    public partial class GeocodingResult
    {
        [JsonProperty("search")]
        public Search Search { get; set; }

        [JsonProperty("displ")]
        public Displ Displ { get; set; }
    }

    public partial class Displ
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("onMove")]
        public string OnMove { get; set; }

        [JsonProperty("center")]
        public double[] Center { get; set; }

        [JsonProperty("zoom")]
        public long Zoom { get; set; }

        [JsonProperty("bbox")]
        public Bbox Bbox { get; set; }
    }

    public partial class Bbox
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public double[][] Coordinates { get; set; }
    }

    public partial class Search
    {
        [JsonProperty("geo")]
        public Geo Geo { get; set; }

        [JsonProperty("yp")]
        public Geo Yp { get; set; }

        [JsonProperty("wp")]
        public Geo Wp { get; set; }
    }

    public partial class Geo
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("mapPoint")]
        public Geometry MapPoint { get; set; }

        [JsonProperty("routePoint")]
        public Geometry RoutePoint { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("navigation")]
        public bool Navigation { get; set; }

        [JsonProperty("wpCount")]
        public long WpCount { get; set; }

        [JsonProperty("ypCount")]
        public long YpCount { get; set; }

        [JsonProperty("features")]
        public Feature[] Features { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("pageSize")]
        public long PageSize { get; set; }

        [JsonProperty("hits")]
        public long Hits { get; set; }

        [JsonProperty("totalHits")]
        public long TotalHits { get; set; }
    }

    public partial class Feature
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("mapPoint")]
        public Geometry MapPoint { get; set; }

        [JsonProperty("routePoint")]
        public Geometry RoutePoint { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("navigation")]
        public bool Navigation { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("postcode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Postcode { get; set; }

        [JsonProperty("area")]
        public string Area { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public partial class Geometry
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; }
    }

    public enum TypeEnum { Point };

    public partial class GeocodingResult
    {
        public static GeocodingResult FromJson(string json) => JsonConvert.DeserializeObject<GeocodingResult>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this GeocodingResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
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
