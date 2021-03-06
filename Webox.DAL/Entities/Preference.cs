namespace Webox.DAL.Entities
{
    public class Preference
    {
        public string PreferenceId { get; set; }

        public string AccountId { get; set; }
        public UserAccount Account { get; set; }

        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
