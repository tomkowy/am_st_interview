using AmStInter.DataSource.Models;

namespace AmStInter.Application.Tests.Orders.Factories
{
    public class LineFactory
    {
        public static Line CreateLine(string merchantProductNo, int quantity)
        {
            return new Line { MerchantProductNo = merchantProductNo, Quantity = quantity };
        }
    }
}
