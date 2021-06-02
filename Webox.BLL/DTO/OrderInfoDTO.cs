using System;

namespace Webox.BLL.DTO
{
    public class OrderInfoDTO
    {
        public string OrderId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public DateTime PlacementDateTime { get; set; }
        public float Price { get; set; }
        public string AccountId { get; set; }
        public OrderItemDTO[] OrderItems { get; set; }
    }
}
