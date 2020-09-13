using Newtonsoft.Json;

namespace WebAPIDotNetCore.Api.Models
{
    public class HouseModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
