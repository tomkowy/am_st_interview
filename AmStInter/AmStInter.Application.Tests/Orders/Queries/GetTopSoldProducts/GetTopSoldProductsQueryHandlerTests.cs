using AmStInter.Application.Orders.Queries.GetTopSoldProducts;
using AmStInter.Application.Tests.Mocks;
using AmStInter.Application.Tests.Orders.Factories;
using AutoFixture.Xunit2;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AmStInter.Application.Tests.Orders.Queries.GetTopSoldProducts
{
    public class GetTopSoldProductsQueryHandlerTests
    {
        private readonly DataSourceMock dataSourceMock = new DataSourceMock();
        private readonly TopSoldProductServiceMock topSoldProductServiceMock = new TopSoldProductServiceMock();

        [Theory]
        [InlineAutoData]
        public async Task Should_FillProductsNames(string no1, string no2, string no3, string no4, string no5, string no6)
        {
            dataSourceMock.GetProductsAsyncMock(ProductFactory.CreateProducts(no1, no2, no3, no4, no5, no6));

            topSoldProductServiceMock.GetOrderTopSoldProducts(LineFactory.CreateLines(
                LineFactory.CreateLine(no1),
                LineFactory.CreateLine(no2),
                LineFactory.CreateLine(no3),
                LineFactory.CreateLine(no4),
                LineFactory.CreateLine(no5)
                ));
            var handler = new GetTopSoldProductsQueryHandler(dataSourceMock.Object, topSoldProductServiceMock.Object);

            var products = await handler.Handle(new GetTopSoldProductsQuery(), CancellationToken.None);

            Assert.All(products, x => Assert.NotNull(x.Name));
        }
    }
}
