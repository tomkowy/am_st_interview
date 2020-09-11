using AmStInter.Application.Orders.Commands;
using AmStInter.Application.Orders.Commands.UpdateStock;
using AmStInter.Application.Orders.Queries.GetInProgressOrders;
using AmStInter.Application.Orders.Queries.GetTopSoldProducts;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmStInter.WebApp.Data
{
    public class OrdersService
    {
        private readonly IMediator _mediator;

        public OrdersService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<InProgressOrderVM>> GetOrders()
        {
            var orders = await _mediator.Send(new GetInProgressOrdersQuery());
            return orders;
        }

        public async Task<IEnumerable<TopSoldProductVM>> GetTopSoldProducts()
        {
            var products = await _mediator.Send(new GetTopSoldProductsQuery());
            return products;
        }

        public async Task<bool> UpdateProductStock(string merchantNumber)
        {
            try
            {
                await _mediator.Send(new UpdateStockCommand(merchantNumber));
                return true;
            }
            catch (UpdatedMerchantProductNoDoesNotExistException)
            {
                return false;
            }
        }
    }
}
