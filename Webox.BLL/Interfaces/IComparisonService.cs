using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IComparisonService
    {
        Task AddComparison(string userName, string laptopId);
        Task RemoveComparison(string id);
        Task<List<ComparisonDTO>> GetComparisons(string userName);
        Task<bool> CheckComparisonsPresence(string userName, string laptopId);
    }
}
