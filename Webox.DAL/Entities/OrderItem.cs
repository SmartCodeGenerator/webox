using System.ComponentModel.DataAnnotations.Schema;

namespace Webox.DAL.Entities
{
    public class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderItemId { get; set; }
        public int Amount { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }

        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
