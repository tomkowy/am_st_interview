using AmStInter.DataSource.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmStInter.DataSource.DataSources
{
    public interface IDataSource
    {
        Task<IEnumerable<Order>> GetInProgressOrdersAsync();
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
