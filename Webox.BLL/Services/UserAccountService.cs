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

        public async Task<string> Login(LoginDTO data)
        {
            var user = await userManager.FindByNameAsync(data.Email);

            if (user != null)
            {
                var verificationResult = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, data.Password);
                if (verificationResult == PasswordVerificationResult.Success || verificationResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    return await TokenProvider.GetJwtToken(user.UserName, userManager);
                }
                return null;
            }

            return null;
        }

        public async Task<string> Register(RegisterDTO data)
        {
            var user = new UserAccount
            { 
                FirstName = data.FirstName, 
                LastName = data.LastName, 
                UserName = data.Email, 
                Email = data.Email, 
                ProfileImagePath = data.ProfileImagePath
            };

            var result = await userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                var role = data.IsEmployee ? "employee" : "client";
                var roleAsignmentResult = await userManager.AddToRoleAsync(user, role);
                if (roleAsignmentResult.Succeeded)
                {
                    return await TokenProvider.GetJwtToken(user.UserName, userManager);
                }
                return null;
            }
            return null;
        }

        public async Task<AccountInformationDTO> GetUserAccountInformation(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var userRoles = await userManager.GetRolesAsync(user);
            bool IsEmployee = userRoles.Contains("employee");
            return new AccountInformationDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.UserName,
                IsEmployee = IsEmployee,
                ProfileImagePath = user.ProfileImagePath
            };
        }
    }
}
