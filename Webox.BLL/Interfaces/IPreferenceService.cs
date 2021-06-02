using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IPreferenceService
    {
        Task AddPreference(string userName, string laptopId);
        Task RemovePreference(string id);
        Task<List<PreferenceDTO>> GetPreferences(string userName);
        Task<bool> CheckPreferencesPresence(string userName, string laptopId);
    }
}
