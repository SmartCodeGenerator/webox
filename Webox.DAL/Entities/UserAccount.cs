using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Webox.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImagePath { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Preference> Preferences { get; set; }
        public ICollection<Comparison> Comparisons { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
