namespace Webox.DAL.Entities
{
    public class Comparison
    {
        public string ComparisonId { get; set; }
        public bool IsOptimal { get; set; }

        public string AccountId { get; set; }
        public UserAccount Account { get; set; }

        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
