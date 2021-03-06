using System.Collections.Generic;

namespace Webox.DAL.Entities
{
    public class Deliverer
    {
        public string DelivererId { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MainOfficeAddress { get; set; }

        public ICollection<StorageLot> StorageLots { get; set; }
    }
}
