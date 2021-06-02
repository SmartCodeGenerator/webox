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
    public class PreferenceService : IPreferenceService
    {
        private readonly UserManager<UserAccount> userManager;
        private readonly UserAccountRepository userAccount;
        private readonly IRepository<Preference> preferences;
        private readonly SaveChangesAsync saveChangesAsync;

        public PreferenceService(UserManager<UserAccount> userManager, IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            userAccount = unitOfWork.UserAccount;
            preferences = unitOfWork.Preferences;
            saveChangesAsync = unitOfWork.SaveChangesAsync;
        }

        public async Task AddPreference(string userName, string laptopId)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var preference = new Preference
                {
                    AccountId = user.Id,
                    LaptopId = laptopId,
                };

                await preferences.Add(preference);
                await saveChangesAsync();
            }
        }

        public async Task RemovePreference(string id)
        {
            await preferences.Delete(id);
            await saveChangesAsync();
        }

        public async Task<List<PreferenceDTO>> GetPreferences(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var data = new List<PreferenceDTO>();
            if (user != null)
            {
                var preferenceLaptops = await userAccount.GetPreferenceLaptops(user.Id);
                foreach (var pair in preferenceLaptops)
                {
                    data.Add(new PreferenceDTO
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

        public async Task<bool> CheckPreferencesPresence(string userName, string laptopId)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return await userAccount.CheckLaptopPreferencePresence(user.Id, laptopId);
            }
            return false;
        }
    }
}
