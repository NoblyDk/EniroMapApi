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
            var result = await new EniroMapClient().GeocodingAsync(CountryEnum.DK, TypeEnum.Address,"Falen 18F, 5000 Odense C");
            Assert.True(result != null);
        }

        [Fact]
        public async void TestRouting()
        {
            var client = new EniroMapClient();
            var from = await client.GeocodingAsync(
                 CountryEnum.DK,
                 TypeEnum.Address,
                 "Falen 18D, 5000 Odense C"
            );
            var to = await client.GeocodingAsync(
                 CountryEnum.DK,
                 TypeEnum.Address,
                 "J. B. Winsløws Vej 4, 5000 Odense"
            );
            var result = await client.RoutingAsync(
                 PrefEnum.Fastest,
                 to.Search.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate?.X,
                 to.Search.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate?.Y,
                 from.Search.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate.X,
                 from.Search.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate.Y
                );
            Assert.True(result.TotalLength == 1441);
        }
    }
}
