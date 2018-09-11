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
                Name = "Falen 18F, 5000 Odense C"
            });
            Assert.NotNull(result);
        }

        [Fact]
        public async void TestRouting()
        {
            var client = new EniroMapClient();
            var from = await client.GeocodingAsync(new GeocodingParameters()
            {
                Country = CountryEnum.DK,
                Type = TypeEnum.Address,
                Name = "Falen 18D, 5000 Odense C"
            });
            var to = await client.GeocodingAsync(new GeocodingParameters()
            {
                Country = CountryEnum.DK,
                Type = TypeEnum.Address,
                Name = "J. B. Winsløws Vej 4, 5000 Odense"
            });
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from.Search.GeocodingResponse.Locations.FirstOrDefault()?.AccessRoadCoordinate,
                To = to.Search.GeocodingResponse.Locations.FirstOrDefault()?.AccessRoadCoordinate,
                Pref = PrefEnum.Fastest
            });
            Assert.Equal(1423, result.TotalLength);
        }
    }
}
