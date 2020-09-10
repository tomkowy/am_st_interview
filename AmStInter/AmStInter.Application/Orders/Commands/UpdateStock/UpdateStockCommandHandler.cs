using AmStInter.Application.Orders.Services;
using AmStInter.DataSource.DataSources;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AmStInter.Application.Orders.Commands.UpdateStock
{
    public class UpdateStockCommandHandler : IRequestHandler<UpdateStockCommand>
    {
        private readonly ITopSoldProductService _topSoldProductService;
        private readonly IDataSource _dataSource;

        public UpdateStockCommandHandler(ITopSoldProductService topSoldProductService, IDataSource dataSource)
        {
            _topSoldProductService = topSoldProductService;
            _dataSource = dataSource;
        }

        public async Task<Unit> Handle(UpdateStockCommand request, CancellationToken cancellationToken)
        {
            var products = await _topSoldProductService.GetOrderTopSoldProducts();
            var updatedMerchantNoExist = products.Any(x => x.MerchantProductNo == request.MerchantProductNo);
            if (!updatedMerchantNoExist)
            {
                throw new UpdatedMerchantProductNoDoesNotExistException(request.MerchantProductNo);
            }

            await _dataSource.UpdateProductStock(request.MerchantProductNo, 25);
            return Unit.Value;
        }
    }
}
