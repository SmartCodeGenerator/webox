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

        public async Task<Dictionary<string, Laptop>> GetComparisonLaptops(string userId)
        {
            return await Task.Run(() =>
            {
                var userAccount = users.Include(u => u.Comparisons).ThenInclude(c => c.Laptop).First(u => u.Id.Equals(userId));
                var comparisonLaptops = new Dictionary<string, Laptop>();
                foreach (var comparison in userAccount.Comparisons)
                {
                    comparisonLaptops.Add(comparison.ComparisonId, comparison.Laptop);
                }
                return comparisonLaptops;
            });
        }

        public async Task<bool> CheckLaptopComparisonPresence(string userId, string laptopId)
        {
            return await Task.Run(() =>
            {
                var userAccount = users.Include(u => u.Comparisons).First(u => u.Id.Equals(userId));
                return userAccount.Comparisons.Any(c => c.LaptopId.Equals(laptopId));
            });
        }

        public async Task<Dictionary<string, Laptop>> GetPreferenceLaptops(string userId)
        {
            return await Task.Run(() =>
            {
                var userAccount = users.Include(u => u.Preferences).ThenInclude(p => p.Laptop).First(u => u.Id.Equals(userId));
                var preferenceLaptops = new Dictionary<string, Laptop>();
                foreach (var preference in userAccount.Preferences)
                {
                    preferenceLaptops.Add(preference.PreferenceId, preference.Laptop);
                }
                return preferenceLaptops;
            });
        }

        public async Task<bool> CheckLaptopPreferencePresence(string userId, string laptopId)
        {
            return await Task.Run(() =>
            {
                var userAccount = users.Include(u => u.Preferences).First(u => u.Id.Equals(userId));
                return userAccount.Preferences.Any(p => p.LaptopId.Equals(laptopId));
            });
        }

        public async Task<Dictionary<string, List<OrderItem>>> GetUserOrders(string userId)
        {
            return await Task.Run(() =>
            {
                var userAccount = users.Include(u => u.Orders).ThenInclude(o => o.OrderItems).First(u => u.Id.Equals(userId));
                var data = new Dictionary<string, List<OrderItem>>();
                foreach (var order in userAccount.Orders)
                {
                    data.Add(order.OrderId, order.OrderItems.ToList());
                }
                return data;
            });
        }
    }
}
