using AmStInter.Application.Orders.Commands;
using AmStInter.Application.Orders.Commands.UpdateStock;
using AmStInter.Application.Tests.Mocks;
using AmStInter.Application.Tests.Orders.Factories;
using AutoFixture.Xunit2;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AmStInter.Application.Tests.Orders.Command
{
    public class UpdateStockCommandHandlerTests
    {
        private readonly TopSoldProductServiceMock topSoldProductServiceMock = new TopSoldProductServiceMock();
        private readonly DataSourceMock dataSourceMock = new DataSourceMock();

        [Theory]
        [InlineAutoData]
        public async Task Should_UpdateStockProduct_When_MerchantProductExist(string no1, string no2)
        {
            topSoldProductServiceMock.GetOrderTopSoldProducts(LineFactory.CreateLines(
                LineFactory.CreateLine(no1),
                LineFactory.CreateLine(no2)
                ));
            var handler = new UpdateStockCommandHandler(topSoldProductServiceMock.Object, dataSourceMock.Object);

            await handler.Handle(new UpdateStockCommand(no2), CancellationToken.None);
        }

        [Theory]
        [InlineAutoData]
        public void Should_ThrowException_When_MerchantProductDoesNotExist(string no1)
        {
            var handler = new UpdateStockCommandHandler(topSoldProductServiceMock.Object, dataSourceMock.Object);

            async Task action() => await handler.Handle(new UpdateStockCommand(no1), CancellationToken.None);

            Assert.ThrowsAsync<UpdatedMerchantProductNoDoesNotExistException>(action);
        }
    }
}
