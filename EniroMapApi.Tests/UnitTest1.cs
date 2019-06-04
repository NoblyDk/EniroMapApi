using System;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace EniroMapApi.Tests
{
    public class UnitTest1
    {
        private readonly EniroHttpClient _eniroHttpClient;
        public UnitTest1()
        {
            _eniroHttpClient = new EniroHttpClient(new HttpClient()
            {
                BaseAddress = new Uri("http://map.eniro.no")
            }, new EniroMapClientConfiguration() {
                PartnerId = ""
            });
        }

        [Fact]
        public async void TestGeocoding()
        {
            var result = await new EniroMapClient(_eniroHttpClient).GeocodingAsync("Falen 18F, Odense C");
            Assert.NotEmpty(result.Search.GeocodingResponse.Locations);
        }
        [Fact]
        public async void TestGeocoding2()
        {
            var result = await new EniroMapClient(_eniroHttpClient).GeocodingAsync("Falen 18F, 5000 Odense C");
            Assert.NotEmpty(result.Search.GeocodingResponse.Locations);
        }
        [Fact]
        public async void TestRouting()
        {
            var client = new EniroMapClient(_eniroHttpClient);
            var from = await client.GeocodingAsync("Ravsted Hovedgade 36, Bylderup-Bov");
            var to = await client.GeocodingAsync("Sydvang 1, 6400 Sønderborg");
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from.Search.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate,
                To = to?.Search?.GeocodingResponse?.Locations.FirstOrDefault().AccessRoadCoordinate,
                Pref = PrefEnum.Fastest
            });
            var distance = result.TotalLength;
            Assert.Equal(49902, distance);
        }


        [Fact]
        public async void TestRouting2()
        {
            var client = new EniroMapClient(_eniroHttpClient);
            var from = await client.GeocodingAsync("Kongshøj Alle 29, Kerteminde");
            var to = await client.GeocodingAsync("Baagøes Alle 15, Svendborg");
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from?.Search?.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate,
                To = to?.Search?.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate,
                Pref = PrefEnum.Fastest
            });
            var distance = result.TotalLength;
            Assert.Equal(59646, distance);
        }
        [Fact]
        public async void TestRouting3()
        {
            var client = new EniroMapClient(_eniroHttpClient);
            var from = await client.GeocodingAsync("Kongshøj Alle 29, Kerteminde");
            var to = await client.GeocodingAsync("Baagøes Alle 15, Svendborg");
            var result = await client.RoutingAsync(new RoutingParameters()
            {
                From = from?.Search?.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate,
                To = to?.Search?.GeocodingResponse.Locations.FirstOrDefault().AccessRoadCoordinate,
                Pref = PrefEnum.Shortest
            });
            var distance = result.TotalLength;
            Assert.Equal(52404, distance);
        }
    }
}
