using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class OrderDTO
    {
        [Required]
        public string DeliveryAddress { get; set; }
        [Range(0, float.MaxValue)]
        public float Price { get; set; }
        public OrderItemDTO[] OrderItems { get; set; }
    }
}
