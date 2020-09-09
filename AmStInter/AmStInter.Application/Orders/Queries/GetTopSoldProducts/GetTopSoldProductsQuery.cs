using MediatR;
using System.Collections.Generic;

namespace AmStInter.Application.Orders.Queries.GetTopSoldProducts
{
    public class GetTopSoldProductsQuery : IRequest<IEnumerable<TopSoldProductVM>>
    {
    }
}
