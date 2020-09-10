namespace AmStInter.Application.Orders.Queries.GetTopSoldProducts
{
    public class TopSoldProductVM
    {
        public string Name { get; set; }
        public string EAN { get; set; }
        public int TotalQuantity { get; set; }
        public string MerchantProductNo { get; set; }

        public TopSoldProductVM(string name, string eAN, int totalQuantity, string merchantProductNo)
        {
            Name = name;
            EAN = eAN;
            TotalQuantity = totalQuantity;
            MerchantProductNo = merchantProductNo;
        }
    }
}
