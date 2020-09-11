using AmStInter.DataSource.DataSources;
using AmStInter.DataSource.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmStInter.Application.Orders.Services
{
    public class TopSoldProductService : ITopSoldProductService
    {
        private readonly IDataSource _dataSource;

        public TopSoldProductService(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<IEnumerable<Line>> GetOrderTopSoldProducts()
        {
            var orders = await _dataSource.GetInProgressOrdersAsync();
            var products = orders.SelectMany(x => x.Lines).GroupBy(x => x.MerchantProductNo).
                Select(x => CreateLine(x))
                .OrderByDescending(x => x.Quantity)
                //I added additional ordering by MerchantProductNo for the situation when 5th and 6th product have the same quantity
                .ThenBy(x => x.MerchantProductNo).Take(5);
            return products;
        }

        private Line CreateLine(IGrouping<string, Line> groupedLine)
        {
            return new Line
            {
                Description = groupedLine.First().Description,
                LineTotalInclVat = groupedLine.Sum(y => y.LineTotalInclVat),
                MerchantProductNo = groupedLine.Key,
                Quantity = groupedLine.Sum(y => y.Quantity),
                Status = groupedLine.First().Status,
                UnitPriceInclVat = groupedLine.First().UnitPriceInclVat
            };
        }
    }
}
