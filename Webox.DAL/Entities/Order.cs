using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webox.DAL.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderId { get; set; }
        public string DeliveryAddress { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public DateTime PlacementDateTime { get; set; }
        public float Price { get; set; }
        
        public string AccountId { get; set; }
        public UserAccount Account { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
