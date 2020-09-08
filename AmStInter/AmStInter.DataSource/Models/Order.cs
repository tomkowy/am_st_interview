using System;
using System.Collections.Generic;

namespace AmStInter.DataSource.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalInclVat { get; set; }
        public IEnumerable<Line> Lines { get; set; }
    }
}
