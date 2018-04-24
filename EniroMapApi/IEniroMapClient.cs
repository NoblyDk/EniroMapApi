using System.Threading.Tasks;

namespace EniroMapApi
{
	public interface IEniroMapClient
	{
		Task<GeocodingResult> GeocodingAsync(GeocodingParameters geocodingParameters);
		Task<RoutingResult> RoutingAsync(RoutingParameters routingParameters);
	}
}