using MediatR;

namespace AmStInter.Application.Orders.Commands.UpdateStock
{
    public class UpdateStockCommand : IRequest
    {
        public string MerchantProductNo { get; set; }

        public UpdateStockCommand(string merchantProductNo)
        {
            MerchantProductNo = merchantProductNo;
        }
    }
}
