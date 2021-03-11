using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Webox.DAL.Entities;

namespace Webox.BLL.Infrastructure
{
    public static class TokenProvider
    {
        private static async Task<ClaimsIdentity> GetIdentity(string userName, UserManager<UserAccount> userManager)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var roles = await userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName)
                };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
                }
                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }

        public static async Task<string> GetJwtToken(string userName, UserManager<UserAccount> userManager)
        {
            var identity = await GetIdentity(userName, userManager);

            if (identity != null)
            {
                var currentDateTime = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: currentDateTime,
                        claims: identity.Claims,
                        expires: currentDateTime.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                    );
                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }

            return null;
        }
    }
}
