using AmStInter.Application.Orders.Services;
using AmStInter.Application.Tests.Mocks;
using AmStInter.Application.Tests.Orders.Factories;
using AutoFixture.Xunit2;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AmStInter.Application.Tests.Orders.Services
{
    public class TopSoldProductServiceTests
    {
        private readonly DataSourceMock dataSourceMock = new DataSourceMock();

        [Theory]
        [InlineAutoData]
        public async Task Should_ReturnOnlyFiveProducts_When_DataSourceContainsMoreThanFive(string no1, string no2, string no3, string no4, string no5, string no6)
        {
            dataSourceMock.GetInProgressOrdersAsyncMock(OrderFactory.CreateOrders(
                OrderFactory.CreateOrdersWithLines(no1),
                OrderFactory.CreateOrdersWithLines(no2, no3),
                OrderFactory.CreateOrdersWithLines(no4),
                OrderFactory.CreateOrdersWithLines(no5),
                OrderFactory.CreateOrdersWithLines(no6)
                ));
            dataSourceMock.GetProductsAsyncMock(ProductFactory.CreateProducts(no1, no2, no3, no4, no5, no6));
            var topSoldProductService = new TopSoldProductService(dataSourceMock.Object);

            var products = await topSoldProductService.GetOrderTopSoldProducts();

            Assert.Equal(5, products.Count());
        }

        [Theory]
        [InlineAutoData]
        public async Task Should_ReturnLessThanFiveProducts_When_DataSourceContainsLessThanFive(string no1, string no2, string no3, string no4)
        {
            dataSourceMock.GetInProgressOrdersAsyncMock(OrderFactory.CreateOrders(
                OrderFactory.CreateOrdersWithLines(no1),
                OrderFactory.CreateOrdersWithLines(no2, no3),
                OrderFactory.CreateOrdersWithLines(no4)
                ));
            var topSoldProductService = new TopSoldProductService(dataSourceMock.Object);

            var products = await topSoldProductService.GetOrderTopSoldProducts();

            Assert.Equal(4, products.Count());
        }

        [Theory]
        [InlineAutoData]
        public async Task Should_SortProductsByTotalQuantity(string no1, string no2, string no3, string no4)
        {
            dataSourceMock.GetInProgressOrdersAsyncMock(OrderFactory.CreateOrders(
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no1, 3)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no2, 1), LineFactory.CreateLine(no3, 4)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no4, 10))
                ));
            var topSoldProductService = new TopSoldProductService(dataSourceMock.Object);

            var products = await topSoldProductService.GetOrderTopSoldProducts();

            Assert.Collection(products,
                x => Assert.Equal(no4, x.MerchantProductNo),
                x => Assert.Equal(no3, x.MerchantProductNo),
                x => Assert.Equal(no1, x.MerchantProductNo),
                x => Assert.Equal(no2, x.MerchantProductNo)
                );
        }

        [Theory]
        [InlineAutoData]
        public async Task Should_ReturnSeparateProducts_When_OrderContainsMoreThanOneLine(string no1, string no2, string no3, string no4)
        {
            dataSourceMock.GetInProgressOrdersAsyncMock(OrderFactory.CreateOrders(
                OrderFactory.CreateOrdersWithLines(no1),
                OrderFactory.CreateOrdersWithLines(no2, no3),
                OrderFactory.CreateOrdersWithLines(no4)
                ));
            var topSoldProductService = new TopSoldProductService(dataSourceMock.Object);

            var products = await topSoldProductService.GetOrderTopSoldProducts();

            Assert.Equal(4, products.Count());
        }

        [Theory]
        [InlineAutoData]
        public async Task Should_ReturnTopSoldProducts(string no1, string no2, string no3, string no4, string no5, string no6)
        {
            dataSourceMock.GetInProgressOrdersAsyncMock(OrderFactory.CreateOrders(
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no1, 1)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no2, 3), LineFactory.CreateLine(no3, 4)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no4, 10)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no5, 5), LineFactory.CreateLine(no6, 8)))
                );
            var topSoldProductService = new TopSoldProductService(dataSourceMock.Object);

            var products = await topSoldProductService.GetOrderTopSoldProducts();

            Assert.Collection(products,
                x => Assert.Equal(no4, x.MerchantProductNo),
                x => Assert.Equal(no6, x.MerchantProductNo),
                x => Assert.Equal(no5, x.MerchantProductNo),
                x => Assert.Equal(no3, x.MerchantProductNo),
                x => Assert.Equal(no2, x.MerchantProductNo)
                );
        }

        [Theory]
        [InlineAutoData]
        public async Task Should_ReturnProductByMerchantProductNo_When_FifthAndSixthHaveSameQuantity(string no1, string no2, string no3, string no4, string no5, string no6)
        {
            dataSourceMock.GetInProgressOrdersAsyncMock(OrderFactory.CreateOrders(
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no1, 3)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no2, 3), LineFactory.CreateLine(no3, 4)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no4, 10)),
                OrderFactory.CreateOrdersWithLines(LineFactory.CreateLine(no5, 5), LineFactory.CreateLine(no6, 8)))
                );
            var topSoldProductService = new TopSoldProductService(dataSourceMock.Object);

            var products = await topSoldProductService.GetOrderTopSoldProducts();

            Assert.Collection(products,
                x => Assert.Equal(no4, x.MerchantProductNo),
                x => Assert.Equal(no6, x.MerchantProductNo),
                x => Assert.Equal(no5, x.MerchantProductNo),
                x => Assert.Equal(no3, x.MerchantProductNo),
                x => Assert.Equal(no1, x.MerchantProductNo)
                );
        }
    }
}
