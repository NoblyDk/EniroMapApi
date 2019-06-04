using System.Threading.Tasks;

namespace EniroMapApi
{
	public class EniroMapClient : IEniroMapClient
	{
        private readonly IEniroHttpClient _eniroHttpClient;

        public EniroMapClient(IEniroHttpClient eniroHttpClient)
		{
            _eniroHttpClient = eniroHttpClient;
        }

		public Task<EniroGeoResult> GeocodingAsync(string addressQuery)
		{
            return _eniroHttpClient.GeocodingAsync(addressQuery);
		}

		public Task<EniroRoutingResult> RoutingAsync(RoutingParameters routingParameters)
		{
            return _eniroHttpClient.RoutingAsync(routingParameters);
		}
	}
}
