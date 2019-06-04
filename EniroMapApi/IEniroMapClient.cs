using System.Threading.Tasks;

namespace EniroMapApi
{
	public interface IEniroMapClient
	{
		Task<EniroGeoResult> GeocodingAsync(string addressQuery);
		Task<EniroRoutingResult> RoutingAsync(RoutingParameters routingParameters);
	}
}