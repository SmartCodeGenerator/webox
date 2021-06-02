using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IStorageLotService
    {
        Task<int> GetLaptopStorageAmount(string laptopId);
        Task SaveStorageLot(StorageLotDTO data);
        Task UpdateStorageLot(string id, StorageLotDTO data);
        Task ReplenishStorageLot(string id, StorageReplenishmentDTO data);
        Task DeleteStorageLot(string id);
        Task<List<StorageLotInfoDTO>> GetStorageLots();
        Task<StorageLotInfoDTO> GetStorageLot(string id);
    }
}
