using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webox.DAL.Entities
{
    public class StorageLot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StorageLotId { get; set; }
        public string WarehouseAddress { get; set; }
        public DateTime SupplyDateTime { get; set; }
        public int LaptopsAmount { get; set; }
        public float LaptopsCostPerUnit { get; set; }
        
        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }

        public string DelivererId { get; set; }
        public Deliverer Deliverer { get; set; }
    }
}
