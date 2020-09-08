using MediatR;
using System.Collections.Generic;

namespace AmStInter.Application.Orders.Queries.GetInProgressOrders
{
    public class GetInProgressOrdersQuery : IRequest<IEnumerable<OrderVM>>
    {
    }
}
