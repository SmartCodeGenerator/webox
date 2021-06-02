using System.ComponentModel.DataAnnotations.Schema;

namespace Webox.DAL.Entities
{
    public class Comparison
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ComparisonId { get; set; }

        public string AccountId { get; set; }
        public UserAccount Account { get; set; }

        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
