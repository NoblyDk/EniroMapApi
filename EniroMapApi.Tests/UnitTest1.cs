using System;
using System.Linq;
using Xunit;

namespace EniroMapApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void TestGeocoding()
        {
            var result = await new EniroMapClient().GeocodingAsync(new GeocodingParameters()
            {
                Country = CountryEnum.DK,
                Type = TypeEnum.Address,
                Name = "Glibingvej 1, 8350 Hundslund"
            });
            Assert.True(result != null);
        }

        [Fact]
        public async void TestRouting()
        {
            var from = await new EniroMapClient().GeocodingAsync(new GeocodingParameters()
            {
                Country = CountryEnum.DK,
                Type = TypeEnum.Address,
                Name = "Glibingvej 1, 8350 Hundslund"
            });
            var to = await new EniroMapClient().GeocodingAsync(new GeocodingParameters()
            {
                Country = CountryEnum.DK,
                Type = TypeEnum.Address,
                Name = "J. B. Winsløws Vej 4, 5000 Odense"
            });
            var result = await new EniroMapClient().RoutingAsync(new RoutingParameters()
            {
                From = from.Search.GeocodingResponse.Locations.FirstOrDefault().PlacementCoordinate,
                To = to.Search.GeocodingResponse.Locations.FirstOrDefault().PlacementCoordinate,
                Pref = PrefEnum.Fastest
            });
            Assert.True(result != null);
        }
    }
}
