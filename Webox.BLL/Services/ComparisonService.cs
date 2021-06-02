using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;
using Webox.DAL.Interfaces;
using Webox.DAL.Repositories;
using static Webox.BLL.Infrastructure.Delegates;

namespace Webox.BLL.Services
{
    public class ComparisonService : IComparisonService
    {
        private readonly UserManager<UserAccount> userManager;
        private readonly UserAccountRepository userAccount;
        private readonly IRepository<Comparison> comparisons;
        private readonly SaveChangesAsync saveChangesAsync;

        public ComparisonService(UserManager<UserAccount> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            userAccount = unitOfWork.UserAccount;
            comparisons = unitOfWork.Comparisons;
            saveChangesAsync = unitOfWork.SaveChangesAsync;
        }

        public async Task AddComparison(string userName, string laptopId)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var comparison = new Comparison
                {
                    AccountId = user.Id,
                    LaptopId = laptopId,
                };

                await comparisons.Add(comparison);
                await saveChangesAsync();
            }
        }

        public async Task RemoveComparison(string id)
        {
            await comparisons.Delete(id);
            await saveChangesAsync();
        }

        public async Task<List<ComparisonDTO>> GetComparisons(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var data = new List<ComparisonDTO>();
            if (user != null)
            {
                var comparisonLaptops = await userAccount.GetComparisonLaptops(user.Id);
                foreach (var pair in comparisonLaptops)
                {
                    data.Add(new ComparisonDTO
                    {
                        Id = pair.Key,
                        LaptopData = new LaptopWithIdDTO
                        {
                            Id = pair.Value.LaptopId,
                            ModelName = pair.Value.ModelName,
                            Manufacturer = pair.Value.Manufacturer,
                            Processor = pair.Value.Processor,
                            Graphic = pair.Value.GraphicsCard,
                            Ram = pair.Value.RAMCapacity,
                            Ssd = pair.Value.SSDCapacity,
                            Screen = pair.Value.ScreenSize,
                            Os = pair.Value.OS,
                            Weight = pair.Value.Weight,
                            Price = pair.Value.Price,
                            Rating = pair.Value.Rating,
                            IsAvailable = pair.Value.IsAvailable,
                            ModelImagePath = pair.Value.ModelImagePath,
                        },
                    });
                }
            }
            return data;
        }

        public async Task<bool> CheckComparisonsPresence(string userName, string laptopId)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return await userAccount.CheckLaptopComparisonPresence(user.Id, laptopId);
            }
            return false;
        }
    }
}
