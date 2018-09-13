﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EniroMapApi.Geocoding;
using EniroMapApi.Routing;
using EniroMapApi.Search;
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
		    queryString += $"&index=rsug";
			queryString += $"&q={System.Net.WebUtility.UrlEncode(addressQuery)}";
			
			var searchResultJson = await _httpClientGeo.GetStringAsync($"search/{queryString}");
            var searchResult = JsonConvert.DeserializeObject<SearchResult>(searchResultJson);
		    
		    queryString = "search.json";
		    queryString += $"?profile=dk_krak";
		    queryString += $"&index=geo";
		    queryString += $"&id={searchResult.Search.Rsug.Features.Select(x => x.Id).FirstOrDefault()}";
			
		    var geocodingResultJson = await _httpClientGeo.GetStringAsync($"search/{queryString}");
		    var geocodingResult = JsonConvert.DeserializeObject<GeocodingResult>(geocodingResultJson);
		    return geocodingResult;


		}

		public async Task<RoutingResult> RoutingAsync(RoutingParameters routingParameters)
		{

			var queryString = "route.json?";
			switch (routingParameters.Pref)
			{
				case PrefEnum.Fastest:
					queryString += "pref=fastest";
					break;
				case PrefEnum.Shortest:
					queryString += "pref=shortest";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			queryString += $"&waypoints={routingParameters.From[0]},{routingParameters.From[1]};{routingParameters.To[0]},{routingParameters.To[1]}";
			queryString += $"&instr=true";
			queryString += $"&res=305";
		
			var result = await _httpClientRoute.GetStringAsync($"route/{queryString}");
			return JsonConvert.DeserializeObject<RoutingResult>(result);


		}
	}
}
