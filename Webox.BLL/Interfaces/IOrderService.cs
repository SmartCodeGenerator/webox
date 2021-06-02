using System.Collections.Generic;
using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IOrderService
    {
        Task MakeOrder(string userName, OrderDTO data);
        Task CancelOrder(string id);
        Task<List<OrderInfoDTO>> GetOrders(string userName);
        Task<OrderInfoDTO> GetOrder(string id);
    }
}
