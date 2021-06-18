using System.Collections.Generic;
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
        Task<List<string>> GetModelNameList(string name);
        Task<List<string>> GetManufacturers();
        Task<List<string>> GetProcessors();
        Task<List<string>> GetGraphics();
        Task<List<int>> GetRAM();
        Task<List<int>> GetSSD();
        Task<List<float>> GetScreens();
        Task<List<string>> GetOS();
        Task<float> GetMinWeight();
        Task<float> GetMaxWeight();
        Task<float> GetMinPrice();
        Task<float> GetMaxPrice();
    }
}
