using System;

namespace AmStInter.DataSource.Exceptions
{
    public class StockUpdateException : Exception
    {
        public StockUpdateException(string merchantNo) : base($"Product '{merchantNo}' stock wasn't updated")
        {
        }
    }
}
