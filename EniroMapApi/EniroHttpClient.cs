using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EniroMapApi
{
    public class EniroHttpClient : IEniroHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly EniroMapClientConfiguration _eniroMapClientConfiguration;

        public EniroHttpClient(HttpClient httpClient, EniroMapClientConfiguration eniroMapClientConfiguration)
        {
            _httpClient = httpClient;
            _eniroMapClientConfiguration = eniroMapClientConfiguration;
        }

        public async Task<EniroGeoResult> GeocodingAsync(string addressQuery)
        {
            var queryString = $"/api/geocode?country=dk&name={System.Net.WebUtility.UrlEncode(addressQuery)}&contentType=json&hits=1&partnerId={_eniroMapClientConfiguration.PartnerId}";

            return JsonConvert.DeserializeObject<EniroGeoResult>(await _httpClient.GetStringAsync(queryString));
        }

        public async Task<EniroRoutingResult> RoutingAsync(RoutingParameters routingParameters)
        {

            var queryString = "/api/route?";
            switch (routingParameters.Pref)
            {
                case PrefEnum.Fastest:
                    queryString += "pref=fastest";
                    break;
                case PrefEnum.Shortest:
                    queryString += "pref=shortest";
                    break;
                default:
                    queryString += "pref=fastest";
                    break;
            }

            queryString += $"&waypoints={routingParameters.From.X.ToString(CultureInfo.InvariantCulture)},{routingParameters.From.Y.ToString(CultureInfo.InvariantCulture)};{routingParameters.To.X.ToString(CultureInfo.InvariantCulture)},{routingParameters.To.Y.ToString(CultureInfo.InvariantCulture)}";
            queryString += $"&instr=true";
            queryString += $"&geo=true";
            queryString += $"&res=200";

            queryString += $"&contentType=json";
            queryString += $"&partnerId={_eniroMapClientConfiguration.PartnerId}";
            var apiResult = await _httpClient.GetStringAsync(queryString);
            return JsonConvert.DeserializeObject<EniroRoutingResult>(apiResult);
        }
    }
}
