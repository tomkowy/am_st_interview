using AmStInter.DataSource.Models;
using System.Collections.Generic;
using System.Linq;

namespace AmStInter.Application.Tests.Orders.Factories
{
    internal static class OrderFactory
    {
        public static IEnumerable<Order> CreateOrders(params Order[] orders)
        {
            return orders;
        }

        public static Order CreateOrdersWithLines(params Line[] lines)
        {
            return new Order { Lines = lines };
        }

        public static Order CreateOrdersWithLines(params string[] merchantNumbers)
        {
            var lines = merchantNumbers.Select(x => new Line { MerchantProductNo = x });
            return new Order { Lines = lines };
        }
    }
}
