using System.Threading.Tasks;
using EniroMapApi.Geocoding;
using EniroMapApi.Routing;

namespace EniroMapApi
{
	public interface IEniroMapClient
	{
		Task<GeocodingResult> GeocodingAsync(string addressQuery);
		Task<RoutingResult> RoutingAsync(RoutingParameters routingParameters);
	}
}