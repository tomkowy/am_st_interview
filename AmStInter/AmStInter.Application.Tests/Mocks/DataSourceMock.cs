using AmStInter.DataSource.DataSources;
using AmStInter.DataSource.Models;
using Moq;
using System.Collections.Generic;

namespace AmStInter.Application.Tests.Mocks
{
    internal class DataSourceMock : Mock<IDataSource>
    {
        public DataSourceMock GetInProgressOrdersAsyncMock(IEnumerable<Order> output)
        {
            Setup(x => x.GetInProgressOrdersAsync()).ReturnsAsync(output);
            return this;
        }

        public DataSourceMock GetProductsAsyncMock(IEnumerable<Product> output)
        {
            Setup(x => x.GetProductsAsync()).ReturnsAsync(output);
            return this;
        }
    }
}
