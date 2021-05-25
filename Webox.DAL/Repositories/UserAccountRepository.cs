using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webox.DAL.Database;
using Webox.DAL.Entities;

namespace Webox.DAL.Repositories
{
    public class UserAccountRepository
    {
        private readonly DbSet<UserAccount> users;

        public UserAccountRepository(ApplicationDbContext context)
        {
            users = context.Users;
        }

        public async Task<string> GetFullName(string userId)
        {
            return await Task.Run(() =>
            {
                var user = users.Find(userId);
                return user != null ? $"{user.FirstName} {user.LastName}" : "";
            });
        }

        public async Task<List<Review>> GetReviews(string userId)
        {
            return await Task.Run(() =>
            {
                var userAccount = users.Include(u => u.Reviews).First(u => u.Id.Equals(userId));
                return userAccount.Reviews.OrderByDescending(r => r.PublishDateTime).ToList();
            });
        }
    }
}
