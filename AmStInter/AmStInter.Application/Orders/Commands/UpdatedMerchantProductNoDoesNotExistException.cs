using System;

namespace AmStInter.Application.Orders.Commands
{
    public class UpdatedMerchantProductNoDoesNotExistException : Exception
    {
        public UpdatedMerchantProductNoDoesNotExistException(string merchantProductNo) : base($"Updated merchant product no '{merchantProductNo}' does not exist")
        {
        }
    }
}
