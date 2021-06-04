using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
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

        public async Task EditUserInfo(string userName, EditUserInfoDTO data)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;

                await userManager.UpdateAsync(user);
            }
        }

        public async Task UpdateUserProfileImage(string userName, string profileImagePath)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                user.ProfileImagePath = profileImagePath;

                await userManager.UpdateAsync(user);
            }
        }

        private static string GenerateVerificationCode()
        {
            var codeBuilder = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 6; i++)
            {
                codeBuilder.Append(random.Next(10));
            }
            return codeBuilder.ToString();
        }

        public async Task<string> GetEmailUpdateVerificationCode(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var code = GenerateVerificationCode();
                await EmailService.SendEmailAsync(user.Email, "Зміна електронної пошти", $"Код верифікації - <b>{code}</b>");
                return code;
            }
            return null;
        }

        public async Task UpdateUserEmail(string userName, string email)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var result = await userManager.SetUserNameAsync(user, email);
                if (result.Succeeded)
                {
                    await userManager.SetEmailAsync(user, email);
                }
            }
        }

        public async Task ChangeUserPassword(string userName, ChangePasswordDTO data)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                if (data.NewPassword.Equals(data.ConfirmNewPassword))
                {
                    await userManager.ChangePasswordAsync(user, data.CurrentPassword, data.NewPassword);
                }
            }
        }

        private static string GenerateUserPassword()
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var passwordBuilder = new StringBuilder();
            var random = new Random();
            for (int i = 0; i < 12; i++)
            {
                passwordBuilder.Append(alphabet[random.Next(alphabet.Length)]);
            }
            return passwordBuilder.ToString();
        }

        public async Task RestoreUserPassword(string email)
        {
            var user = await userManager.FindByNameAsync(email);

            if (user != null)
            {
                var newPassword = GenerateUserPassword();
                var newPasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword);
                user.PasswordHash = newPasswordHash;
                await userManager.UpdateAsync(user);
                await EmailService.SendEmailAsync(email, "Відновлення забутого пароля", $"Ваш новий пароль - <b>{newPassword}</b>.\nРекомендуємо одразу змінити його на бажаний для Вас пароль в особистому кабінеті.");
            }
        }

        public async Task<string> GetPasswordResetVerificationCode(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var code = GenerateVerificationCode();
                await EmailService.SendEmailAsync(user.Email, "Скидання пароля", $"Код верифікації - <b>{code}</b>");
                return code;
            }
            return null;
        }

        public async Task ResetUserPassword(string userName, ResetPasswordDTO data)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                if (data.ConfirmNewPassword.Equals(data.NewPassword))
                {
                    var newPasswordHash = userManager.PasswordHasher.HashPassword(user, data.NewPassword);
                    user.PasswordHash = newPasswordHash;
                    await userManager.UpdateAsync(user);
                }
            }
        }
    }
}
