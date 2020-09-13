using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebAPIDotNetCore.Api.Clients;
using Xunit;

namespace WebAPIDotNetCore.IntegrationTest.Clients
{
    public class PotterAPIClientTest
    {
        [Fact]
        public async Task GetHousesAsync()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            var configuration = builder.Build();

            var potterApiClient = new PotterAPIClient(new HttpClient(), configuration);

            var houses = await potterApiClient.GetHousesAsync();

            Assert.NotEmpty(houses);
        }

        [Fact]
        public async Task GetHouseAsync()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            var configuration = builder.Build();

            var potterApiClient = new PotterAPIClient(new HttpClient(), configuration);

            var house = await potterApiClient.GetHouseAsync("5a05e2b252f721a3cf2ea33f");

            Assert.NotNull(house);
        }
    }
}
