using System;
using System.ComponentModel.DataAnnotations;

namespace Webox.BLL.DTO
{
    public class StorageLotDTO
    {
        [Required]
        public string WarehouseAddress { get; set; }
        [Required, Range(1, 500000)]
        public float LaptopsCostPerUnit { get; set; }
        [Required]
        public string LaptopId { get; set; }
        [Required]
        public string DelivererId { get; set; }
    }
}
