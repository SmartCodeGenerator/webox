using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webox.DAL.Entities
{
    public class Deliverer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DelivererId { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MainOfficeAddress { get; set; }

        public ICollection<StorageLot> StorageLots { get; set; }
    }
}
