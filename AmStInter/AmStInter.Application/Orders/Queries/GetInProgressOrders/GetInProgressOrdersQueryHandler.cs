using AmStInter.DataSource.DataSources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AmStInter.Application.Orders.Queries.GetInProgressOrders
{
    public class GetInProgressOrdersQueryHandler : IRequestHandler<GetInProgressOrdersQuery, IEnumerable<InProgressOrderVM>>
    {
        private readonly IDataSource _dataSource;

        public GetInProgressOrdersQueryHandler(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<IEnumerable<InProgressOrderVM>> Handle(GetInProgressOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _dataSource.GetInProgressOrdersAsync();
            return orders.Select(x => new InProgressOrderVM(x));
        }
    }
}
