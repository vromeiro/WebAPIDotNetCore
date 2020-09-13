using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebAPIDotNetCore.Api.Models;

namespace WebAPIDotNetCore.Api.Clients
{
    public class PotterAPIClient : IPotterAPIClient
    {
        private readonly HttpClient client;
        private readonly string url;
        private readonly string key;

        public PotterAPIClient(HttpClient client, IConfiguration configuration)
        {
            this.client = client ?? throw new ArgumentNullException("client");
            this.url = configuration["PotterAPI:Url"] ?? throw new ArgumentNullException("configuration");
            this.key = configuration["PotterAPI:Key"] ?? throw new ArgumentNullException("configuration");
        }

        public async Task<IList<HouseModel>> GetHousesAsync(int errorCount = 1)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/houses?key={key}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode == false)
            {
                if (errorCount < 3)
                {
                    errorCount++;
                    return await GetHousesAsync(errorCount);
                }
                else
                {
                    return null;
                }
            }

            return await ConvertJsonToObject(response);
        }

        public async Task<HouseModel> GetHouseAsync(string id, int errorCount = 1)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{url}/houses/{id}/?key={key}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode == false)
            {
                if (errorCount < 3)
                {
                    errorCount++;
                    return await GetHouseAsync(id, errorCount);
                }
                else
                {
                    return null;
                }
            }

            var houses = await ConvertJsonToObject(response);
            return houses?.FirstOrDefault();
        }

        private async Task<IList<HouseModel>> ConvertJsonToObject(HttpResponseMessage response)
        {
            try
            {
                var contentString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<HouseModel>>(contentString);
            }
            catch
            {
                return null;
            }
        }
    }
}
