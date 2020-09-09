namespace AmStInter.Application.Orders.Queries.GetTopSoldProducts
{
    public class TopSoldProductVM
    {
        public string Name { get; set; }
        public string EAN { get; set; }
        public int TotalQuantity { get; set; }

        public TopSoldProductVM(string name, string eAN, int totalQuantity)
        {
            Name = name;
            EAN = eAN;
            TotalQuantity = totalQuantity;
        }
    }
}
