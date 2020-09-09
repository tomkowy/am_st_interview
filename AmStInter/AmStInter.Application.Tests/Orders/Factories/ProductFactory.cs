using AmStInter.DataSource.Models;
using System.Collections.Generic;
using System.Linq;

namespace AmStInter.Application.Tests.Orders.Factories
{
    internal static class ProductFactory
    {
        public static IEnumerable<Product> CreateProducts(params Product[] products)
        {
            return products;
        }

        public static IEnumerable<Product> CreateProducts(params string[] merchantNumbers)
        {
            var products = merchantNumbers.Select(x => new Product { MerchantProductNo = x, Name = x });
            return products;
        }
    }
}
