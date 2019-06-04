using System.Threading.Tasks;

namespace EniroMapApi
{
    public interface IEniroHttpClient
    {
        Task<EniroGeoResult> GeocodingAsync(string addressQuery);
        Task<EniroRoutingResult> RoutingAsync(RoutingParameters routingParameters);
    }
}