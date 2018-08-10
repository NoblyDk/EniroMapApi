using System.Threading.Tasks;

namespace EniroMapApi
{
	public interface IEniroMapClient
	{
		Task<GeocodingResult> GeocodingAsync(CountryEnum country, TypeEnum type, string name);
		Task<RoutingResult> RoutingAsync(PrefEnum pref, string toX, string toY, string fromX, string fromY);
	}
}