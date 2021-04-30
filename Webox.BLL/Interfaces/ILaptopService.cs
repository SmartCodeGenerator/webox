using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Infrastructure;
using Webox.BLL.Infrastructure.QueryParams;

namespace Webox.BLL.Interfaces
{
    public interface ILaptopService
    {
        Task AddLaptop(LaptopDTO data);
        Task DeleteLaptop(string id);
        Task<LaptopWithIdDTO> GetById(string id);
        Task<PaginatedList<LaptopWithIdDTO>> Index(SortOrder sortOrder, LaptopParams queryParams, int pageIndex);
        Task UpdateLaptop(string id, LaptopDTO data);
    }
}
