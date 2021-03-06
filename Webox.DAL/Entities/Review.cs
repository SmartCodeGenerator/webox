using System;

namespace Webox.DAL.Entities
{
    public class Review
    {
        public string ReviewId;
        public string ReviewText;
        public DateTime PublishDateTime { get; set; }
        public float Rating { get; set; }

        public string AccountId { get; set; }
        public UserAccount Account { get; set; }

        public string LaptopId { get; set; }
        public Laptop Laptop { get; set; }
    }
}
