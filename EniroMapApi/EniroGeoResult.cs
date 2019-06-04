using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EniroMapApi
{
    public class EniroGeoResult
    {
        [JsonProperty("search", NullValueHandling = NullValueHandling.Ignore)]
        public Search Search { get; set; }
    }

    public class Search
    {
        [JsonProperty("geocodingResponse", NullValueHandling = NullValueHandling.Ignore)]
        public GeocodingResponse GeocodingResponse { get; set; }
    }

    public class GeocodingResponse
    {
        [JsonProperty("locations", NullValueHandling = NullValueHandling.Ignore)]
        public List<Location> Locations { get; set; }

        [JsonProperty("totalHits", NullValueHandling = NullValueHandling.Ignore)]
        public string TotalHits { get; set; }

        [JsonProperty("searchMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchMessage { get; set; }

        [JsonProperty("searchType", NullValueHandling = NullValueHandling.Ignore)]
        public string SearchType { get; set; }

        [JsonProperty("xanthochromic", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Xanthochromic { get; set; }

        [JsonProperty("resultQuality", NullValueHandling = NullValueHandling.Ignore)]
        public string ResultQuality { get; set; }
    }

    public class Location
    {
        [JsonProperty("addressId", NullValueHandling = NullValueHandling.Ignore)] 
        public string AddressId { get; set; }

        [JsonProperty("locationType", NullValueHandling = NullValueHandling.Ignore)]
        public string LocationType { get; set; }

        [JsonProperty("roadname", NullValueHandling = NullValueHandling.Ignore)]
        public string Roadname { get; set; }

        [JsonProperty("housenumber", NullValueHandling = NullValueHandling.Ignore)]
        public string Housenumber { get; set; }

        [JsonProperty("placename", NullValueHandling = NullValueHandling.Ignore)]
        public string Placename { get; set; }

        [JsonProperty("zip", NullValueHandling = NullValueHandling.Ignore)]     
        public string Zip { get; set; }

        [JsonProperty("postarea", NullValueHandling = NullValueHandling.Ignore)]
        public string Postarea { get; set; }

        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }

        [JsonProperty("municipality", NullValueHandling = NullValueHandling.Ignore)]
        public string Municipality { get; set; }

        [JsonProperty("municipalityId", NullValueHandling = NullValueHandling.Ignore)]   
        public string MunicipalityId { get; set; }

        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        [JsonProperty("placementCoordinate", NullValueHandling = NullValueHandling.Ignore)]
        public Coordinate PlacementCoordinate { get; set; }

        [JsonProperty("accessRoadCoordinate", NullValueHandling = NullValueHandling.Ignore)]
        public Coordinate AccessRoadCoordinate { get; set; }

        [JsonProperty("boundingRectangle", NullValueHandling = NullValueHandling.Ignore)]
        public BoundingRectangle BoundingRectangle { get; set; }

        [JsonProperty("source_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SourceId { get; set; }

        [JsonProperty("cmp_id", NullValueHandling = NullValueHandling.Ignore)]     
        public string CmpId { get; set; }
    }

    public class Coordinate
    {
        [JsonProperty("EPSG", NullValueHandling = NullValueHandling.Ignore)]
        public double Epsg { get; set; }

        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        public double X { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        public double Y { get; set; }
    }

    public class BoundingRectangle
    {
        [JsonProperty("EPSG", NullValueHandling = NullValueHandling.Ignore)]
        public double Epsg { get; set; }

        [JsonProperty("minX", NullValueHandling = NullValueHandling.Ignore)]
        public double MinX { get; set; }

        [JsonProperty("minY", NullValueHandling = NullValueHandling.Ignore)]
        public double MinY { get; set; }

        [JsonProperty("maxX", NullValueHandling = NullValueHandling.Ignore)]
        public double MaxX { get; set; }

        [JsonProperty("maxY", NullValueHandling = NullValueHandling.Ignore)]
        public double MaxY { get; set; }
    }
}
