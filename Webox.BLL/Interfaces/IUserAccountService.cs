using System.Threading.Tasks;
using Webox.BLL.DTO;

namespace Webox.BLL.Interfaces
{
    public interface IUserAccountService
    {
        Task<string> Login(LoginDTO data);
        Task<string> Register(RegisterDTO data);
        Task<AccountInformationDTO> GetUserAccountInformation(string userName);
        Task EditUserInfo(string userName, EditUserInfoDTO data);
        Task UpdateUserProfileImage(string userName, string profileImagePath);
        Task<string> GetEmailUpdateVerificationCode(string userName);
        Task UpdateUserEmail(string userName, string email);
        Task ChangeUserPassword(string userName, ChangePasswordDTO data);
        Task RestoreUserPassword(string email);
        Task<string> GetPasswordResetVerificationCode(string userName);
        Task ResetUserPassword(string userName, ResetPasswordDTO data);
        void Dispose();
    }
}
