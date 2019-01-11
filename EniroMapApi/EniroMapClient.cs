using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EniroMapApi.Geocoding;
using EniroMapApi.Routing;
using Newtonsoft.Json;

namespace EniroMapApi
{
	public class EniroMapClient : IEniroMapClient
	{
		private HttpClient _httpClientGeo;
	    private HttpClient _httpClientRoute;
		public EniroMapClient()
		{
			_httpClientGeo = new HttpClient(new HttpClientHandler()
			{
			
			})
			{
				BaseAddress = new Uri("https://mapsearch.eniro.com/")
			};
		    _httpClientRoute = new HttpClient(new HttpClientHandler()
		    {
			
		    })
		    {
		        BaseAddress = new Uri("https://route.enirocdn.com/")
		    };
		}

		public async Task<GeocodingResult> GeocodingAsync(string addressQuery)
		{

			var queryString = "search.json";
		    queryString += $"?profile=dk_krak";
		    queryString += $"&index=geo";
		    queryString += $"&q={System.Net.WebUtility.UrlEncode(addressQuery)}";
			
            var response = await _httpClientGeo.GetAsync($"search/{queryString}");
		    var geocodingResultJson = await response.Content.ReadAsStringAsync();
		    if (!response.IsSuccessStatusCode)
		    {
		        throw new Exception($"Error calling {_httpClientGeo.BaseAddress.AbsoluteUri}, Response was {response.StatusCode} - {geocodingResultJson}");
		    }
		   
		    var geocodingResult = JsonConvert.DeserializeObject<GeocodingResult>(geocodingResultJson);
		    if (geocodingResult.Search.Geo.Type == "FeatureCollection")
		    {
		        geocodingResult.Search.Geo.RoutePoint =
		            geocodingResult.Search.Geo.Features.FirstOrDefault()?.RoutePoint;
		    }
		    return geocodingResult;
		}

		public async Task<RoutingResult> RoutingAsync(RoutingParameters routingParameters)
		{

			var queryString = "route.json?";
			switch (routingParameters.Pref)
			{
				case PrefEnum.Fastest:
					queryString += "pref=FASTEST";
					break;
				case PrefEnum.Shortest:
					queryString += "pref=SHORTEST";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			queryString += $"&waypoints={routingParameters.From[0].ToString(CultureInfo.InvariantCulture)},{routingParameters.From[1].ToString(CultureInfo.InvariantCulture)};{routingParameters.To[0].ToString(CultureInfo.InvariantCulture)},{routingParameters.To[1].ToString(CultureInfo.InvariantCulture)}";
			queryString += $"&instr=true";
			queryString += $"&res=305";
		
		    var response = await _httpClientRoute.GetAsync($"route/{queryString}");
		    var result = await response.Content.ReadAsStringAsync();
		    if (!response.IsSuccessStatusCode)
		    {
		        throw new Exception($"Error calling {_httpClientRoute.BaseAddress.AbsoluteUri}, Response was {response.StatusCode} - {result}");
		    }
		 
			return JsonConvert.DeserializeObject<RoutingResult>(result);


		}
	}
}
