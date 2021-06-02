using System;

namespace Webox.BLL.DTO
{
    public class StorageLotInfoDTO
    {
        public string StorageLotId { get; set; }
        public string WarehouseAddress { get; set; }
        public DateTime SupplyDateTime { get; set; }
        public int LaptopsAmount { get; set; }
        public float LaptopsCostPerUnit { get; set; }
        public string LaptopId { get; set; }
        public string DelivererId { get; set; }
    }
}
