using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EniroMapApi
{
	public class EniroMapClient : IEniroMapClient
	{
		private HttpClient _httpClient;

		public EniroMapClient()
		{
			_httpClient = new HttpClient(new HttpClientHandler()
			{
			
			})
			{
				BaseAddress = new Uri("https://map.eniro.no/api/")
			};
		}

		public async Task<GeocodingResult> GeocodingAsync(GeocodingParameters geocodingParameters)
		{

			var queryString = "";
			switch (geocodingParameters.Country)
			{
				case CountryEnum.SE:
					queryString += "?country=se";
					break;
				case CountryEnum.DK:
					queryString += "?country=dk";
					break;
				case CountryEnum.FI:
					queryString += "?country=fi";
					break;
				case CountryEnum.NO:
					queryString += "?country=no";
					break;
				case CountryEnum.ALL:
					queryString += "?country=all";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			switch (geocodingParameters.Type)
			{
				case TypeEnum.Any:
					queryString += "&type=any";
					break;
				case TypeEnum.Address:
					queryString += "&type=address";
					break;
				case TypeEnum.Street:
					queryString += "&type=street";
					break;
				case TypeEnum.city:
					queryString += "&type=city";
					break;
				default:
					queryString += "&type=any";
					break;
			}

			queryString += $"&name={geocodingParameters.Name}";
			queryString += $"&contentType=json";
			var result = await _httpClient.GetStringAsync($"geocode/{queryString}");
			return JsonConvert.DeserializeObject<GeocodingResult>(result);


		}

		public async Task<RoutingResult> RoutingAsync(RoutingParameters routingParameters)
		{

			var queryString = "";
			switch (routingParameters.Pref)
			{
				case PrefEnum.Fastest:
					queryString += "?pref=fastest";
					break;
				case PrefEnum.Shortest:
					queryString += "?pref=shortest";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			queryString += $"&waypoints={routingParameters.To.X},{routingParameters.To.Y};{routingParameters.From.X},{routingParameters.From.Y}";
			queryString += $"&instr=false";
			queryString += $"&geo=false";
			queryString += $"&contentType=json";
			var result = await _httpClient.GetStringAsync($"route/{queryString}");
			return JsonConvert.DeserializeObject<RoutingResult>(result);


		}
	}
}
