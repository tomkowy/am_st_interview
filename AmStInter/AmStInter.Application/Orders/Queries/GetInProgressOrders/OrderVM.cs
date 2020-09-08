using AmStInter.DataSource.Models;
using System;

namespace AmStInter.Application.Orders.Queries.GetInProgressOrders
{
    public class OrderVM
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalInclVat { get; set; }

        public OrderVM(Order order)
        {
            Id = order.Id;
            ChannelId = order.ChannelId;
            ChannelName = order.ChannelName;
            Status = order.Status;
            CreatedAt = order.CreatedAt;
            TotalInclVat = order.TotalInclVat;
        }
    }
}
