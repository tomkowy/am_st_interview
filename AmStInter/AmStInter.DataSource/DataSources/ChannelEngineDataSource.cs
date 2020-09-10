using AmStInter.DataSource.Exceptions;
using AmStInter.DataSource.Models;
using AmStInter.DataSource.Models.Requests;
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

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}products?apikey={_apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            var contentProduct = JsonConvert.DeserializeObject<ProductContent>(json);
            return contentProduct.Content;
        }

        public async Task UpdateProductStock(string merchantNo, int value)
        {
            var body = new PutBody<int>(value, "Stock", "replace");
            var httpContent = new StringContent(body.ToHttpBody());
            var response = await _httpClient.PatchAsync($"{_apiUrl}products/{merchantNo}?apikey={_apiKey}", httpContent);
            var json = await response.Content.ReadAsStringAsync();
            var contentProduct = JsonConvert.DeserializeObject<ResponsBody>(json);

            if (!contentProduct.Success)
            {
                throw new StockUpdateException(merchantNo);
            }
        }
    }
}
