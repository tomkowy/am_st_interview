using AmStInter.DataSource.DataSources;
using AmStInter.DataSource.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AmStInter.DataSource.Tests.DataSources
{
    public class ChannelEngineDataSourceTests
    {
        private const string ApiUrl = "https://api-dev.channelengine.net/api/v2/";
        private const string ApiKey = "541b989ef78ccb1bad630ea5b85c6ebff9ca3322";

        [Fact]
        public async Task Should_GetOrdersFromChannelEngine()
        {
            var dataSource = new ChannelEngineDataSource(ApiUrl, ApiKey);

            var orders = await dataSource.GetInProgressOrdersAsync();

            Assert.NotEmpty(orders);
        }

        [Fact]
        public async Task Should_ReturnOnlyOrdersInProgress()
        {
            var dataSource = new ChannelEngineDataSource(ApiUrl, ApiKey);

            var orders = await dataSource.GetInProgressOrdersAsync();

            Assert.All(orders, x => Assert.Equal("IN_PROGRESS", x.Status));
        }

        [Fact]
        public async Task Should_GetProductsFromChannelEngine()
        {
            var dataSource = new ChannelEngineDataSource(ApiUrl, ApiKey);

            var products = await dataSource.GetProductsAsync();

            Assert.NotEmpty(products);
        }

        [Fact]
        public async Task Should_UpdateProductStockInChannelEngine()
        {
            var dataSource = new ChannelEngineDataSource(ApiUrl, ApiKey);
            var merchantNo = "001201";
            var value = 10;

            await dataSource.UpdateProductStock(merchantNo, value);
        }

        [Fact]
        public void Should_ThrowExcepion_When_UpdateProductStockDoesNotExistInChannelEngine()
        {
            var dataSource = new ChannelEngineDataSource(ApiUrl, ApiKey);
            var merchantNo = "";
            var value = 10;

            async Task action() => await dataSource.UpdateProductStock(merchantNo, value);

            Assert.ThrowsAsync<StockUpdateException>(action);
        }
    }
}
