using System.Collections.Generic;

namespace Webox.DAL.Entities
{
    public class UserAccount
    {
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmployee { get; set; }
        public string ProfileImagePath { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Preference> Preferences { get; set; }
        public ICollection<Comparison> Comparisons { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
