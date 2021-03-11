using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Webox.BLL.DTO;
using Webox.BLL.Infrastructure;
using Webox.BLL.Interfaces;
using Webox.DAL.Entities;

namespace Webox.BLL.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserManager<UserAccount> userManager;

        public UserAccountService(UserManager<UserAccount> userManager)
        {
            this.userManager = userManager;
        }

        public void Dispose()
        {
            userManager.Dispose();
        }

        public async Task<object> Login(LoginDTO data)
        {
            var user = await userManager.FindByNameAsync(data.Email);

            if (user != null)
            {
                var verificationResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, data.Password);
                if (verificationResult == PasswordVerificationResult.Success || verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    var jwt = await TokenProvider.GetJwtToken(user.UserName, userManager);
                    return new { accessToken = jwt, profileImagePath = user.ProfileImagePath, userName = user.UserName };
                }
                return null;
            }

            return null;
        }
    }
}
