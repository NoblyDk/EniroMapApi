using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi
{
   public partial class GeocodingResult
    {
        [JsonProperty("search")]
        public Search Search { get; set; }
    }

    public partial class Search
    {
        [JsonProperty("geocodingResponse")]
        public GeocodingResponse GeocodingResponse { get; set; }
    }

    public partial class GeocodingResponse
    {
        [JsonProperty("locations")]
        public Location[] Locations { get; set; }

        [JsonProperty("totalHits")]
        public long TotalHits { get; set; }

        [JsonProperty("searchMessage")]
        public string SearchMessage { get; set; }

        [JsonProperty("searchType")]
        public string SearchType { get; set; }

        [JsonProperty("xanthochromic")]
        public bool Xanthochromic { get; set; }

        [JsonProperty("resultQuality")]
        public string ResultQuality { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("addressId")]
        public string AddressId { get; set; }

        [JsonProperty("locationType")]
        public string LocationType { get; set; }

        [JsonProperty("roadname")]
        public string Roadname { get; set; }

        [JsonProperty("housenumber")]
        public string Housenumber { get; set; }

        [JsonProperty("placename")]
        public string Placename { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("postarea")]
        public string Postarea { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("municipalityId")]
        public string MunicipalityId { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("placementCoordinate")]
        public Coordinate PlacementCoordinate { get; set; }

        [JsonProperty("accessRoadCoordinate")]
        public Coordinate AccessRoadCoordinate { get; set; }

        [JsonProperty("boundingRectangle")]
        public BoundingRectangle BoundingRectangle { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }

        [JsonProperty("cmp_id")]
        public string CmpId { get; set; }
    }

    public partial class Coordinate
    {
        [JsonProperty("EPSG")]
        public long Epsg { get; set; }

        [JsonProperty("x")]
        public string X { get; set; }

        [JsonProperty("y")]
        public string Y { get; set; }
    }

    public partial class BoundingRectangle
    {
        [JsonProperty("EPSG")]
        public long Epsg { get; set; }

        [JsonProperty("minX")]
        public string MinX { get; set; }

        [JsonProperty("minY")]
        public string MinY { get; set; }

        [JsonProperty("maxX")]
        public string MaxX { get; set; }

        [JsonProperty("maxY")]
        public string MaxY { get; set; }
    }

    public partial class GeocodingResult
    {
        public static GeocodingResult FromJson(string json) => JsonConvert.DeserializeObject<GeocodingResult>(json, GeocodingResultConverter.Settings);
    }

    internal static class GeocodingResultConverter
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
