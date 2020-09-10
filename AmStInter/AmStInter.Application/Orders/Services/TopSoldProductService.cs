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
            //I added additional ordering by MerchantProductNo for the situation when 5th and 6th product have the same quantity
            var products = orders.SelectMany(x => x.Lines).OrderByDescending(x => x.Quantity).ThenBy(x => x.MerchantProductNo).Take(5);
            return products;
        }
    }
}
