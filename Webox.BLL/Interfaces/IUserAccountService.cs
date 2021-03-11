using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IUserAccountService
    {
        Task<object> Login(LoginDTO data);
        void Dispose();
    }
}
