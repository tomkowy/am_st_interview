using AmStInter.DataSource.Models;
using System.Collections.Generic;

namespace AmStInter.Application.Tests.Orders.Factories
{
    public class LineFactory
    {
        public static Line CreateLine(string merchantProductNo, int quantity)
        {
            return new Line { MerchantProductNo = merchantProductNo, Quantity = quantity };
        }

        public static Line CreateLine(string merchantProductNo)
        {
            return new Line { MerchantProductNo = merchantProductNo };
        }

        public static IEnumerable<Line> CreateLines(params Line[] orders)
        {
            return orders;
        }
    }
}
