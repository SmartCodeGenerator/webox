using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IUserAccountService
    {
        Task<string> Login(LoginDTO data);
        Task<string> Register(RegisterDTO data);
        Task<AccountInformationDTO> GetUserAccountInformation(string userName);
        void Dispose();
    }
}
