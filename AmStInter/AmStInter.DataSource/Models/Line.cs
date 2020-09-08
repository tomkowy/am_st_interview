namespace AmStInter.DataSource.Models
{
    public class Line
    {
        public string Status { get; set; }
        public string Description { get; set; }
        public string MerchantProductNo { get; set; }
        public decimal LineTotalInclVat { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPriceInclVat { get; set; }

    }
}
