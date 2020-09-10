using AmStInter.Application.Orders.Services;
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
        private readonly ITopSoldProductService _topSoldProductService;

        public GetTopSoldProductsQueryHandler(IDataSource dataSource, ITopSoldProductService topSoldProductService)
        {
            _dataSource = dataSource;
            _topSoldProductService = topSoldProductService;
        }

        public async Task<IEnumerable<TopSoldProductVM>> Handle(GetTopSoldProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _topSoldProductService.GetOrderTopSoldProducts();
            var allProducts = await _dataSource.GetProductsAsync();

            var productsVM = products.Join(allProducts, prod => prod.MerchantProductNo, allProd => allProd.MerchantProductNo,
                (x, y) => new TopSoldProductVM(y.Name, y.EAN, x.Quantity));

            return productsVM;
        }
    }
}
