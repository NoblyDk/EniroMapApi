using System.Linq;
using Xunit;

namespace EniroMapApi.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void TestGeocoding()
        {
            var result = await new EniroMapClient().GeocodingAsync("Falen 18F, Odense C");
            Assert.NotNull(result.Search.Geo.RoutePoint);
        }
        [Fact]
        public async void TestGeocoding2()
        {
            var result = await new EniroMapClient().GeocodingAsync("Falen 18F, 5000 Odense C");
            Assert.NotNull(result.Search.Geo.RoutePoint);
        }
        [Fact]
        public async void TestRouting()
        {
            var client = new EniroMapClient();
            var from = await client.GeocodingAsync("Ravsted Hovedgade 36, Bylderup-Bov");
            var to = await client.GeocodingAsync("Sydvang 1, 6400 Sønderborg");
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from?.Search?.Geo?.RoutePoint?.Coordinates,
                To = to?.Search?.Geo?.RoutePoint?.Coordinates,
                Pref = PrefEnum.Fastest
            });
            var distance = result.RouteGeometries.Features.Select(x => x.Properties.Length).FirstOrDefault();
            Assert.Equal(49642, distance);
        }


        [Fact]
        public async void TestRouting2()
        {
            var client = new EniroMapClient();
            var from = await client.GeocodingAsync("Kongshøj Alle 29, Kerteminde");
            var to = await client.GeocodingAsync("Baagøes Alle 15, Svendborg");
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from?.Search?.Geo?.RoutePoint?.Coordinates,
                To = to?.Search?.Geo?.RoutePoint?.Coordinates,
                Pref = PrefEnum.Fastest
            });
            var distance = result.RouteGeometries.Features.Select(x => x.Properties.Length).FirstOrDefault();
            Assert.Equal(59206, distance);
        }
        [Fact]
        public async void TestRouting3()
        {
            var client = new EniroMapClient();
            var from = await client.GeocodingAsync("Kongshøj Alle 29, Kerteminde");
            var to = await client.GeocodingAsync("Baagøes Alle 15, Svendborg");
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from?.Search?.Geo?.RoutePoint?.Coordinates,
                To = to?.Search?.Geo?.RoutePoint?.Coordinates,
                Pref = PrefEnum.Shortest
            });
            var distance = result.RouteGeometries.Features.Select(x => x.Properties.Length).FirstOrDefault();
            Assert.NotEqual(59206, distance);
        }
    }
}
