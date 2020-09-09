using AmStInter.DataSource.DataSources;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmStInter.Application.Orders.Queries.GetTopSoldProducts
{
    public class GetTopSoldProductsQueryHandler : IRequestHandler<GetTopSoldProductsQuery, IEnumerable<TopSoldProductVM>>
    {
        private readonly IDataSource _dataSource;

        public GetTopSoldProductsQueryHandler(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<IEnumerable<TopSoldProductVM>> Handle(GetTopSoldProductsQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dataSource.GetInProgressOrdersAsync();
            var allProducts = await _dataSource.GetProductsAsync();
            //I added additional ordering by MerchantProductNo for the situation when 5th and 6th product have the same quantity
            var products = orders.SelectMany(x => x.Lines).OrderByDescending(x => x.Quantity).ThenBy(x => x.MerchantProductNo).Take(5);

            var productsVM = products.Join(allProducts, prod => prod.MerchantProductNo, allProd => allProd.MerchantProductNo,
                (x, y) => new TopSoldProductVM(y.Name, y.EAN, x.Quantity));

            return productsVM;
        }
    }
}
