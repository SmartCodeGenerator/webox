using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IDelivererService
    {
        Task SaveDeliverer(DelivererDTO data);
        Task UpdateDeliverer(string id, DelivererDTO data);
        Task DeleteDeliverer(string id);
        Task<List<DelivererInfoDTO>> GetDeliverers();
        Task<DelivererInfoDTO> GetDeliverer(string id);
    }
}
