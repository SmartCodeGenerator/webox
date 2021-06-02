using System;
using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class OrderItemDTO
    {
        [Range(1, 100)]
        public int Amount { get; set; }
        [Required]
        public string LaptopId { get; set; }
    }
}
