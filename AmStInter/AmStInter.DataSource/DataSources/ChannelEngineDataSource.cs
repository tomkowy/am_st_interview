using AmStInter.DataSource.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AmStInter.DataSource.DataSources
{
    public class ChannelEngineDataSource : IDataSource
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public ChannelEngineDataSource(string apiUrl, string apiKey)
        {
            _apiUrl = apiUrl;
            _apiKey = apiKey;
        }

        public async Task<IEnumerable<Order>> GetInProgressOrdersAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}orders?statuses=IN_PROGRESS&apikey={_apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            var contentOrder = JsonConvert.DeserializeObject<OrderContent>(json);
            return contentOrder.Content;
        }
    }
}
