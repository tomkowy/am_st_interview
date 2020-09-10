using AmStInter.Application.Orders.Services;
using AmStInter.DataSource.Models;
using Moq;
using System.Collections.Generic;

namespace AmStInter.Application.Tests.Mocks
{
    internal class TopSoldProductServiceMock : Mock<ITopSoldProductService>
    {
        public TopSoldProductServiceMock GetOrderTopSoldProducts(IEnumerable<Line> output)
        {
            Setup(x => x.GetOrderTopSoldProducts()).ReturnsAsync(output);
            return this;
        }
    }
}
