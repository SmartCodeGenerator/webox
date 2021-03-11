using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Webox.DAL.Entities;

namespace Webox.DAL.Database
{
    public class ApplicationDbInitializer
    {
        public static async Task InitializeAsync(UserManager<UserAccount> userManager, RoleManager<IdentityRole> roleManager)
        {
            var email = "employee1@gmail.com";
            var password = "123456qw";

            if (await roleManager.FindByNameAsync("client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("client"));
            }
            if (await roleManager.FindByNameAsync("employee") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("employee"));
            }

            if (await userManager.FindByNameAsync(email) == null)
            {
                var employee1 = new UserAccount { FirstName = "Oleksandr", LastName = "Mytryniuk", UserName = email, Email = email };
                var result = await userManager.CreateAsync(employee1, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee1, "employee");
                }
            }
        }
    }
}
