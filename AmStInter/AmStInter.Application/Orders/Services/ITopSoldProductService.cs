using AmStInter.DataSource.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmStInter.Application.Orders.Services
{
    public interface ITopSoldProductService
    {
        Task<IEnumerable<Line>> GetOrderTopSoldProducts();
    }
}
